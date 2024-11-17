using Root.Constants;
using Root.Player;
using System;
using System.Collections;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public bool InAttack { get; private set; }

    private Animator _animator;

    [SerializeField] private new Transform transform;

    [SerializeField] private EnemyStats _stats;

    [SerializeField] private EnemyMovement _movement;

    [SerializeField] private WeapongConfig _weapon;

    [SerializeField] private float _attackDelay;

    [SerializeField] private ParticleSystem _moneySource;

    [SerializeField] private ParticleSystem _soulsSource;

    private void Start()
        => Init();

    private void Update() 
        => DetectPlayer();

    private void OnDrawGizmos()
    {
        Gizmos.color = _weapon.Color;

        var offset = _weapon.OriginOffset;

        offset.x = _movement.IsRight ? offset.x : -offset.x;

        var center = transform.position + (Vector3)offset;

        Gizmos.DrawSphere(center, _weapon.OverlapRadius);
    }

    public void Init()
    {
        _animator = GetComponent<Animator>();

        var trigger = GameObject.Find("CollectorTrigger").GetComponent<Collider2D>();

        _moneySource.trigger.SetCollider(0, trigger);

        _soulsSource.trigger.SetCollider(0, trigger);

        RefreshStats();
    }

    public void OnAttack()
    {
        if (!InAttack) return;

        var player = OverlapPlayer();

        if (player == null) return;

        player.GetComponent<PlayerCombat>().ApplyDamage(_weapon.Damage);
    }

    public void OnAttackEnd()
    {
        InAttack = false;

        _movement.ClearSlowdown();
    }

    public void ApplyDamage(int value)
    {
        if (_stats.Health == 0) return;

        _stats.Health -= value;

        _animator.SetTrigger("Hit");

        if(_stats.Health == 0)
        {
            _moneySource.Play();

            _soulsSource.Play();
        }

        RefreshStats();

        InAttack = false;
    }

    private void Attack()
    {
        _animator.SetTrigger("Attack");

        _movement.Slow(_weapon.Slowdown);
    }

    private void DetectPlayer()
    {
        if (InAttack) return;

        var player = OverlapPlayer();

        if (player == null) return;

        InAttack = true;

        StopAllCoroutines();

        StartCoroutine(DelayRoutine(Attack, _attackDelay));
    }

    private Collider2D OverlapPlayer()
    {
        var results = new Collider2D[1];

        var offset = _weapon.OriginOffset;

        offset.x = _movement.IsRight ? offset.x : -offset.x;

        var center = transform.position + (Vector3)offset;

        Physics2D.OverlapCircleNonAlloc(center, _weapon.OverlapRadius, results, GameConstants.PlayerLayerMask);

        return results[0];
    }

    private void RefreshStats()
    {
        _animator.SetInteger("Health", _stats.Health);
    }

    private IEnumerator DelayRoutine(Action callback, float delay)
    {
        yield return new WaitForSeconds(delay);

        callback?.Invoke();
    }
}
