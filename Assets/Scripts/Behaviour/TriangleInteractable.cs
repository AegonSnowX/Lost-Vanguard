using UnityEngine;

public class TriangleInteractable : MonoBehaviour, IInteractable, ICancelableInteractable
{
    private SpriteRenderer sr;
    private InteractionPrompt prompt ;

    private void Awake()
    {
        prompt = GetComponent<InteractionPrompt>();
        sr = GetComponent<SpriteRenderer>();
    }

    public void Interact(PlayerStateController player)
    {
        player.LockAll();
        sr.color = Color.red;
        Debug.Log($"{name} interacted with and player locked.");
        UIManager.Instance.OpenModal();
    }

    public void StopInteract(PlayerStateController player)
    {
        player.UnlockAll();
        sr.color = Color.white;
        Debug.Log($"{name} interaction stopped and player unlocked.");
        UIManager.Instance.CloseModal();
    }
    void IInteractable.Focus()
    {
        sr.color = Color.green;
        Debug.Log($"{name} focused.");
        prompt?.ShowPrompt();
    }
    void IInteractable.Unfocus()
    {
        sr.color = Color.white;
        Debug.Log($"{name} unfocused.");
         prompt?.HidePrompt();
    }
}
