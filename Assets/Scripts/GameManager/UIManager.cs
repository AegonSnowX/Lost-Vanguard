using System;
using UnityEngine;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private GameObject modalRoot;

    [Header("Terminal Panels")]
    [SerializeField] private GameObject communicationPanel;
    [SerializeField] private GameObject reactorPanel;
    [SerializeField] private GameObject masterPanel;
    [SerializeField] private GameObject controlroomPanel;

    private GameObject currentPanel;
    [SerializeField]private GameObject mapPanel;
    public bool IsModalOpen { get; private set; }

public ModalEnum CurrentModal { get; private set; } = ModalEnum.None;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        modalRoot.SetActive(false);
        communicationPanel.SetActive(false);
        reactorPanel.SetActive(false);
        masterPanel.SetActive(false);
        controlroomPanel.SetActive(false);
    }

    public void OpenTerminal(TerminalType type)
    {
        if (IsModalOpen) return;

        IsModalOpen = true;
        modalRoot.SetActive(true);

        switch (type)
        {
            case TerminalType.MasterTerminal:
                ShowPanel(masterPanel);
                break;
            case TerminalType.ReactorTerminal:
                ShowPanel(reactorPanel);
                break;
            case TerminalType.Communication:
                ShowPanel(communicationPanel);
                break;
            case TerminalType.ControlRoom:
                ShowPanel(controlroomPanel);
                break;
                case TerminalType.NavigationTerminal:
                ShowPanel(mapPanel);
                break;
        }
    }

    public void CloseTerminal()
    {
        if (!IsModalOpen) return;

        HideCurrentPanel();
        modalRoot.SetActive(false);
        IsModalOpen = false;
    }

    private void ShowPanel(GameObject panel)
    {
        HideCurrentPanel();
        currentPanel = panel;
        currentPanel.SetActive(true);
    }

    private void HideCurrentPanel()
    {
        if (currentPanel != null)
            currentPanel.SetActive(false);
    }
    public void OpenMap()
{
    if (CurrentModal != ModalEnum.None) return;

    CurrentModal = ModalEnum.Map;
    modalRoot.SetActive(true);
    ShowPanel(mapPanel);
}

public void CloseMap()
{
    if (CurrentModal != ModalEnum.Map) return;

    HideCurrentPanel();
    modalRoot.SetActive(false);
    CurrentModal = ModalEnum.None;
}

}
