using Root.Constants;
using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public bool IsRight
    {
        get => transform.rotation.y == 0;

        private set 
        {
            var current = transform.eulerAngles;

            current.y = value ? 0f : 180f;

            transform.eulerAngles = current;
        }
    }

    private Rigidbody2D _rigidbody;

    private Animator _animator;

    [SerializeField] private float _moveSpeed;

    [SerializeField] private float _slowMultiplier = 1f;

    [SerializeField] private float _horizontalOverlapSize;

    [SerializeField] private float _verticalOverlapSize;

    [SerializeField] private Vector2 _targetOffset;

    [SerializeField] private float _areaX;

    [SerializeField] private Color _overlapColor;


    private void Start()
        => Init();

    private void Update()
        => HandleState();

    private void OnDrawGizmos()
    {
        Gizmos.color = _overlapColor;

        Gizmos.DrawCube(transform.position, new Vector3(_horizontalOverlapSize, _verticalOverlapSize, 1f));
    }

    public void Init()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        _animator = GetComponentInChildren<Animator>();
    }

    public void Slow(float time, float slowdown)
    => StartCoroutine(SlowRoutine(time, slowdown));

    public void Slow(float slowdown)
        => _slowMultiplier = 1f - slowdown;

    public void ClearSlowdown()
        => _slowMultiplier = 1f;

    private void HandleState()
    {
        var results = new Collider2D[1];

        var size = new Vector2(_horizontalOverlapSize, _verticalOverlapSize);

        Physics2D.OverlapBoxNonAlloc(transform.position, size, 0f, results, GameConstants.PlayerLayerMask);

        if (results[0] != null)
        {
            FollowTarget(results[0].gameObject.transform.position + (Vector3)_targetOffset);
        }

        _animator.SetInteger("X", Mathf.RoundToInt(_rigidbody.velocity.x));
    }

    private void FollowTarget(Vector3 position)
    {
        var offset = _targetOffset;

        var isTargetRight = (transform.position - position).normalized.x <= 0;
        
        offset.x = isTargetRight ? offset.x : -offset.x;

        Move((Vector2)position + offset);
    }

    private void Move(Vector2 position)
    {
        var isNear = Vector3.Distance(position, _rigidbody.position) <= _targetOffset.x;

        if (isNear) return;

        var direction = (position - _rigidbody.position).normalized;

        direction.y = _rigidbody.velocity.y;

        _rigidbody.velocity = direction * _moveSpeed * _slowMultiplier;


        if (_slowMultiplier != 0f)
        {
            IsRight = _rigidbody.velocity.x > 0f;
        }
    }

    private IEnumerator SlowRoutine(float time, float slowdown)
    {
        Slow(slowdown);

        yield return new WaitForSeconds(time);

        ClearSlowdown();
    }
}
