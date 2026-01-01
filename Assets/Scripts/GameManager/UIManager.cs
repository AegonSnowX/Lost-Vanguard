using System;
using UnityEngine;
using TMPro;
using System.Text;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private GameObject modalRoot;
    [Header("Ship Status UI")]
[SerializeField] private TextMeshProUGUI shipStatusText;
//[SerializeField] private TextMeshProUGUI Room1StatusText;


    [Header("Terminal Panels")]
    [SerializeField] private GameObject communicationPanel;
    [SerializeField] private GameObject reactorPanel;
    [SerializeField] private GameObject masterPanel;
    [SerializeField] private GameObject controlroomPanel;
    [SerializeField] private GameObject shipstatusPanel;

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
                case TerminalType.ShipStatusTerminal:
                ShowPanel(shipstatusPanel);
                 RefreshShipStatus();
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

private void OnEnable()
{
    if (MasterShipController.Instance != null)
        MasterShipController.Instance.OnShipStatusChanged += RefreshShipStatus;
}

private void OnDisable()
{
    if (MasterShipController.Instance != null)
        MasterShipController.Instance.OnShipStatusChanged -= RefreshShipStatus;
}

private void RefreshShipStatus()
{
    if (shipStatusText == null) return;
    if (MasterShipController.Instance == null) return;

    var rooms = MasterShipController.Instance.GetAllRoomStatuses();
    var sb = new StringBuilder();

    foreach (var room in rooms)
    {
        sb.AppendLine($"{room.Key}: {room.Value}");
    }

    shipStatusText.text = sb.ToString();
}

}
