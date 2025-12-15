using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
   // [SerializeField] private float interactionRadius = 1.2f;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private PlayerStateController stateController;

    private IInteractable currentInteractable;
    private ICancelableInteractable cancelableInteractable;
    private readonly List<IInteractable> interactablesInRange = new();


    void Awake()
    {
        //getting PlayerStateController component
        if (stateController == null)
        stateController = GetComponent<PlayerStateController>();

    }

    void Update()
    {
        // If player is currently interacting with something
        if (!stateController.CanInteract)
    {
        // Check for cancel input 
        if (Input.GetKeyDown(KeyCode.Escape) && cancelableInteractable != null)
        {
            cancelableInteractable.StopInteract(stateController);
            cancelableInteractable = null;
// Refocus on the current interactable if still in range
              if (stateController.CanInteract && currentInteractable != null)
    {
        currentInteractable.Focus();
    }
        }
        return;
    }
// Check for interaction input
    if (Input.GetKeyDown(KeyCode.E) && currentInteractable != null)
    {
        currentInteractable.Interact(stateController);
         currentInteractable.Unfocus();
        cancelableInteractable = currentInteractable as ICancelableInteractable;
    }
  
    }
// Using trigger colliders to detect interactables
 private void OnTriggerEnter2D(Collider2D other)
{
    if (!stateController.CanInteract) return;

    var interactable = other.GetComponent<IInteractable>();
    if (interactable == null) return;

    interactablesInRange.Add(interactable);
    ResolveCurrentInteractable();
}


// Using trigger colliders to detect interactables when exiting
 private void OnTriggerExit2D(Collider2D other)
{
    var interactable = other.GetComponent<IInteractable>();
    if (interactable == null) return;

    interactablesInRange.Remove(interactable);

    if (interactable == currentInteractable)
    {
        currentInteractable.Unfocus();
        currentInteractable = null;
        ResolveCurrentInteractable();
    }
}

    private void ResolveCurrentInteractable()
{
    if (!stateController.CanInteract) return;

    IInteractable best = null;
    float bestDist = float.MaxValue;

    foreach (var interactable in interactablesInRange)
    {
        var mb = interactable as MonoBehaviour;
        if (mb == null) continue;

        float dist = Vector2.Distance(transform.position, mb.transform.position);
        if (dist < bestDist)
        {
            bestDist = dist;
            best = interactable;
        }
    }

    if (best == currentInteractable) return;

    currentInteractable?.Unfocus();
    currentInteractable = best;
    currentInteractable?.Focus();
}

   
}
