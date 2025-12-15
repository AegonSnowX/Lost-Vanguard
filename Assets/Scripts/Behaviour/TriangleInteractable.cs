using UnityEngine;

public class TriangleInteractable : MonoBehaviour, IInteractable, ICancelableInteractable
{
    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void Interact(PlayerStateController player)
    {
        player.LockAll();
        sr.color = Color.red;
        Debug.Log($"{name} interacted with and player locked.");
    }

    public void StopInteract(PlayerStateController player)
    {
        player.UnlockAll();
        sr.color = Color.white;
        Debug.Log($"{name} interaction stopped and player unlocked.");
    }
    void IInteractable.Focus()
    {
        sr.color = Color.green;
        Debug.Log($"{name} focused.");
    }
    void IInteractable.Unfocus()
    {
        sr.color = Color.white;
        Debug.Log($"{name} unfocused.");
    }
}
