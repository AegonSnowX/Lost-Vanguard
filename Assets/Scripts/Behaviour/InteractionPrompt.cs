using UnityEngine;

public class InteractionPrompt: MonoBehaviour
{
    [SerializeField] private GameObject promptObject;

    public void ShowPrompt()
    {
        if (promptObject != null)
        {
            promptObject.SetActive(true);
        }
    }
    public void HidePrompt()
    {
        if (promptObject != null)
        {
            promptObject.SetActive(false);
        }
    }
}
