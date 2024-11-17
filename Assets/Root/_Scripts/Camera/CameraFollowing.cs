using UnityEngine;

public class CameraFollowing : MonoBehaviour
{
    [SerializeField] private Transform _target;

    [SerializeField] private Vector3 _offset;

    [SerializeField] private Vector2 _minPosition;

    [SerializeField] private Vector2 _maxPosition;

    [SerializeField][Range(0, 1f)] private float _smoothness;

    private void Start()
        => Init();

    private void Update()
        => Move(_target.position);

    public void Init()
    {

    }

    private void Move(Vector3 position)
    {
        var toPosition = position + _offset;

        toPosition.x = toPosition.x < _minPosition.x ? _minPosition.x : toPosition.x;

        toPosition.y = toPosition.y < _minPosition.y ? _minPosition.y : toPosition.y;

        transform.position = Vector3.Lerp(transform.position, toPosition, _smoothness);
    }
}
