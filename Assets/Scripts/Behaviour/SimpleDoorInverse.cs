using UnityEngine;

public class SimpleDoorInverse: MonoBehaviour, IInteractable
{
   [SerializeField] GameObject bridgeDoor;
    [SerializeField] private Sprite spriteOpen;
    [SerializeField] private Sprite spriteClosed;
    private SpriteRenderer sr ;
    private BoxCollider2D doorCollider;
    
    private void Awake()
    {
        if (bridgeDoor != null)
        {
            sr=bridgeDoor.GetComponent<SpriteRenderer>();
            doorCollider = bridgeDoor.GetComponent<BoxCollider2D>();
        }
    }

     public void Interact(PlayerStateController player)
    {

        Debug.Log("Door interacted with");
       
        if (sr.sprite == spriteClosed)
        {
            sr.sprite = spriteOpen;
            doorCollider.enabled = true;
        }
        else
        {
            sr.sprite = spriteClosed;
            doorCollider.enabled = false;
        }
    }

    public void Focus()
    {
     //   throw new System.NotImplementedException();
    }

    public void Unfocus()
    {
       // throw new System.NotImplementedException();
    }
}
