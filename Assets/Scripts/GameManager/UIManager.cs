using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
public bool IsModalOpen { get; private set; }
    [SerializeField] private GameObject modalRoot;

    private void Awake()
    {
        Instance = this;
        modalRoot.SetActive(false);
    }

    public void OpenModal()
    {
         if (IsModalOpen) return;

    IsModalOpen = true;
    modalRoot.SetActive(true);
    }

    public void CloseModal()
    {
        if (!IsModalOpen) return;

    IsModalOpen = false;
    modalRoot.SetActive(false);
    }
}
