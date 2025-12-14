using UnityEngine;

public class PlayerStateController : MonoBehaviour
{
    public bool CanMove { get; private set; } = true;
    public bool CanInteract { get; private set; } = true;

    public void LockAll()
    {
        CanMove = false;
        CanInteract = false;
    }

    public void UnlockAll()
    {
        CanMove = true;
        CanInteract = true;
    }

    public void LockMovement()
    {
        CanMove = false;
    }

    public void UnlockMovement()
    {
        CanMove = true;
    }

    public void LockInteraction()
    {
        CanInteract = false;
    }

    public void UnlockInteraction()
    {
        CanInteract = true;
    }
}
