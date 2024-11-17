using Root.Constants;
using System.Collections.Generic;
using UnityEngine;


namespace Root.Player
{
    public class PlayerCombat : MonoBehaviour
    {
        public int WeaponID
        {
            get => _weaponID;
            set
            {
                if (value > _weapons.Count || value < 0) return;

                _weaponID = value;

                _animator.SetInteger("WeaponID", _weaponID);
            }
        }

        private Animator _animator;

        [SerializeField] private int _weaponID;  

        [SerializeField] private List<WeapongConfig> _weapons;

        [SerializeField] private PlayerMovement _movement;

        [SerializeField] private PlayerStats _stats;

        private void Start() 
            => Init();

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && _stats.Health > 0)
            {
                Attack();
            }
        }

        private void OnDrawGizmos()
        {
            var weapon = _weapons[_weaponID];

            Gizmos.color = weapon.Color;

            var offset = weapon.OriginOffset;

            offset.x = _movement.IsRight ? offset.x : -offset.x;

            var center = transform.position + (Vector3) offset;

            Gizmos.DrawSphere(center, weapon.OverlapRadius);
        }

        public void Init()
        {
            _animator = GetComponent<Animator>();

            _animator.SetInteger("Health", _stats.Health);
        }

        public void OnAttack()
        {
            var weapon = _weapons[_weaponID];

            var results = new Collider2D[1];

            var offset = weapon.OriginOffset;

            offset.x = _movement.IsRight ? offset.x : -offset.x;

            var center = transform.position + (Vector3)offset;

            Physics2D.OverlapCircleNonAlloc(center, weapon.OverlapRadius, results, GameConstants.EnemyLayerMask);

            if (results[0] == null) return;

            results[0].GetComponentInChildren<EnemyCombat>().ApplyDamage(weapon.Damage);
        }

        public void OnAttackEnd()
        {
            _movement.ClearSlowdown();
        }

        public void ApplyDamage(int value)
        {
            if (_stats.Health == 0) return;

            _stats.Health -= value;

            _animator.SetTrigger("Hit");

            _animator.SetInteger("Health", _stats.Health);

            _movement.ClearSlowdown();

            if (_stats.Health == 0)
                _movement.Slow(1f);
        }

        private void Attack()
        {
            if (!_movement.IsGrounded) return;

            _movement.Slow(_weapons[_weaponID].Slowdown);

            _animator.SetTrigger($"Attack");
        }
    }
}
