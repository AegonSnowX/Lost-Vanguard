using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
   // [SerializeField] private float interactionRadius = 1.2f;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private PlayerStateController stateController;

    private IInteractable currentInteractable;
    private ICancelableInteractable cancelableInteractable;

    void Awake()
    {
        stateController = GetComponent<PlayerStateController>();

    }

    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.E) && currentInteractable != null && stateController.CanInteract)
        {
            currentInteractable.Interact(stateController);
        }
        
        if(Input.GetKeyDown(KeyCode.Escape)&& !stateController.CanInteract)
        {
             cancelableInteractable.StopInteract(stateController);
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger entered with " + other.name);
        if (((1 << other.gameObject.layer) & interactableLayer) != 0)
        {
            currentInteractable = other.GetComponent<IInteractable>();
            cancelableInteractable = other.GetComponent<ICancelableInteractable>();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<IInteractable>() == currentInteractable)
        {
            currentInteractable = null;
            cancelableInteractable = null;
        }
    }
}
