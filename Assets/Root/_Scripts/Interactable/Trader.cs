using UnityEngine;
using Root.Interactable;

public class Trader : Interactable
{
    [SerializeField] private GameObject ShopWindow;

    public override void Interact()
    {
        Time.timeScale = 0f;

        ShopWindow.SetActive(true);
    }

    public void StopInteract()
    {
        ShopWindow.SetActive(false);

        Time.timeScale = 1f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) IsReady = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) IsReady = false;
    }
}
