using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TerminalDoor : MonoBehaviour,IInteractable ,ICancelableInteractable
{
  

private SpriteRenderer sr;
[SerializeField] Sprite spriteon ;
[SerializeField] Sprite spriteoff ;
//private string terminaltype;
  
    private void Awake()
    {
      sr = GetComponent<SpriteRenderer>();
    }

    public void Interact(PlayerStateController player)
    {
      player.LockAll();
   // UIManager.Instance.OpenModal();
   // UIManager.Instance.OpenTerminal(TerminalType.MasterTerminal);
    
    }

    public void Focus()
    {
     //   sr.sprite = spriteon;
    }

    public void Unfocus()
    {
       // sr.sprite = spriteoff;
    }

    public void StopInteract(PlayerStateController player)
    {
       UIManager.Instance.CloseTerminal();
       player.UnlockAll();
      // UIManager.Instance.CloseModal();
      
    }
}