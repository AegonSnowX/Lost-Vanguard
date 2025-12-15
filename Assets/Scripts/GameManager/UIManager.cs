using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private GameObject modalRoot;

    private void Awake()
    {
        Instance = this;
        modalRoot.SetActive(false);
    }

    public void OpenModal()
    {
        modalRoot.SetActive(true);
    }

    public void CloseModal()
    {
        modalRoot.SetActive(false);
    }
}
