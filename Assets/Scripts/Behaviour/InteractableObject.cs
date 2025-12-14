using UnityEngine;

public class SimpleInteractableObject : MonoBehaviour, IInteractable ,ICancelableInteractable 
{
    public void Interact(PlayerStateController player)
    {

        player =FindAnyObjectByType<PlayerStateController>();
        player.LockAll();
        Debug.Log($"{name} interacted with and player movement locked.");
        GetComponent<SpriteRenderer>().color = Color.red;
    }
    public void StopInteract(PlayerStateController player)
    {
        player = FindAnyObjectByType<PlayerStateController>();
        player.UnlockAll();
        Debug.Log($"{name} interaction stopped and player movement unlocked.");
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    
}
