using UnityEngine;

public class TerminalInteractable : MonoBehaviour, IInteractable, ICancelableInteractable
{
    [Header("Terminal Settings")]
    [SerializeField] private TerminalType terminalType;

    [Header("Optional Visuals")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite idleSprite;
    [SerializeField] private Sprite focusedSprite;

    private bool isActive;

    private void Awake()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Called when player presses interact
    public void Interact(PlayerStateController player)
    {
        if (isActive) return;

        isActive = true;

        player.LockAll();
        UIManager.Instance.OpenTerminal(terminalType);

        Unfocus(); // remove focus visuals while UI is open
    }

    // Called when player cancels (Escape)
    public void StopInteract(PlayerStateController player)
    {
        if (!isActive) return;

        isActive = false;

        UIManager.Instance.CloseTerminal();
        player.UnlockAll();
    }

    // Called when player enters interaction range
    public void Focus()
    {
        if (spriteRenderer != null && focusedSprite != null)
            spriteRenderer.sprite = focusedSprite;
    }

    // Called when player leaves interaction range or interaction starts
    public void Unfocus()
    {
        if (spriteRenderer != null && idleSprite != null)
            spriteRenderer.sprite = idleSprite;
    }
}
