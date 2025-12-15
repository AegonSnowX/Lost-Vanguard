using System.Collections;
using UnityEngine;

public class TerminalDoor : MonoBehaviour,IInteractable
{
  

    private SpriteRenderer sr;
   
   [SerializeField] private GameObject doorToOpen;
   private byte timer=0;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void Interact(PlayerStateController player)
    {
      
        sr.color = Color.red;
        Debug.Log($"{name} interacted with and player locked.");
        StartCoroutine(OpenDoorTemporarily());  
    }
    private IEnumerator OpenDoorTemporarily()
{
    sr.color = Color.red;
    doorToOpen.SetActive(false);

    yield return new WaitForSeconds(5f);

    doorToOpen.SetActive(true);
    sr.color = Color.white;
}


    void IInteractable.Focus()
    {
        sr.color = Color.green;
        Debug.Log($"{name} focused.");
       // prompt?.ShowPrompt();
    }
    void IInteractable.Unfocus()
    {
        sr.color = Color.white;
        Debug.Log($"{name} unfocused.");
        // prompt?.HidePrompt();
    }
}


