using Root.Constants;
using Root.Interactable;
using Root.Player;
using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    [SerializeField] private PlayerStats _stats;

    [SerializeField] private Vector2 _detectionOffset;

    [SerializeField] private Vector2 _detectionOverlapSize;

    [SerializeField] private Color _detectionOverlapColor;

    [SerializeField] private GameObject _info;

    private void Update()
        => DetectInteractable(Input.GetKeyDown(KeyCode.E));

    private void OnDrawGizmos()
    {
        Gizmos.color = _detectionOverlapColor;

        Gizmos.DrawCube(transform.position + (Vector3)_detectionOffset, (Vector3) _detectionOverlapSize);
    }

    private void OnParticleCollision(GameObject other)
    {
        var isMoney = other.CompareTag("Money");

        var isSouls = other.CompareTag("Souls");

        if (isMoney)
            _stats.Money++;

        if (isSouls)
            _stats.Souls++;
    }

    private void DetectInteractable(bool input)
    {
        var results = new Collider2D[1];

        var position = (Vector2) transform.position +  _detectionOffset;

        Physics2D.OverlapBoxNonAlloc(position, _detectionOverlapSize, 0f, results, GameConstants.InteractableLayerMask);

        var result = results[0];

        if (result == null)
        {
            _info.SetActive(false);

            return;
        }

        var interactable = result.GetComponent<Interactable>();

        _info.SetActive(interactable.IsReady);

        if (input) Interact(interactable);
    }

    private void Interact(Interactable interactable)
        => interactable.Interact();
}
