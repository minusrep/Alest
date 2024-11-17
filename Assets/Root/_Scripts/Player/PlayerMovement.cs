using Root.Constants;
using System.Collections;
using UnityEngine;


namespace Root.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        public bool IsRight => !_spriteRenderer.flipX;

        public bool IsGrounded
        {
            get
            {
                var results = new Collider2D[1];

                Physics2D.OverlapBoxNonAlloc((Vector2)transform.position + _originOffset, _overlapSize, 0f, results, GameConstants.GroundLayerMask);

                var isGrounded = results[0] != null;

                return isGrounded;
            }
        }

        private Rigidbody2D _rigidbody;

        private Animator _animator;

        private float _slowMultiplier = 1f;

        [SerializeField] private SpriteRenderer _spriteRenderer;

        [SerializeField] private float _moveSpeed;

        [Header("Jump Settings")]

        [SerializeField] private float _jumpForce;

        [SerializeField] private Vector2 _originOffset;

        [SerializeField] private Vector2 _overlapSize;

        private void Start()
            => Init();

        private void Update()
        {
            Move(Input.GetAxis("Horizontal"));

            if (Input.GetKeyDown(KeyCode.Space))
                Jump();

            UpdateAnimation();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;

            Gizmos.DrawCube((Vector2)transform.position + _originOffset, _overlapSize);
        }


        public void Init()
        {
            _rigidbody = GetComponent<Rigidbody2D>();

            _spriteRenderer = GetComponent<SpriteRenderer>();

            _animator = GetComponent<Animator>();
        }

        public void Move(float input)
        {
            if (input == 0) return;

            _rigidbody.velocity = new Vector2(input * _moveSpeed * _slowMultiplier, _rigidbody.velocity.y);

            if (_slowMultiplier != 0)
                _spriteRenderer.flipX = input < 0;
        }

        public void Jump()
        {
            if (!IsGrounded) return;

            _rigidbody.AddForce(Vector2.up * _jumpForce * _slowMultiplier, ForceMode2D.Impulse);
        }

        public void Slow(float time, float slowdown)
            => StartCoroutine(SlowRoutine(time, slowdown));

        public void Slow(float slowdown)
            => _slowMultiplier = 1f - slowdown;

        public void ClearSlowdown()
            => _slowMultiplier = 1f;

        private void UpdateAnimation()
        {
            _animator.SetInteger("X", (int)_rigidbody.velocity.x);

            _animator.SetInteger("Y", (int)_rigidbody.velocity.y);
        
            _animator.SetBool("IsGrounded", IsGrounded);
        }


        private IEnumerator SlowRoutine(float time, float slowdown)
        {
            Slow(slowdown);

            yield return new WaitForSeconds(time);

            ClearSlowdown();
        }
    }
}

