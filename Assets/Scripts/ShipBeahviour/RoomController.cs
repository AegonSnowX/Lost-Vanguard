using UnityEngine;

public abstract class RoomController : MonoBehaviour
{
    [Header("Room Info")]
    [SerializeField] private string roomId;
    [SerializeField] protected RoomStatus currentStatus = RoomStatus.Normal;

    public string RoomId => roomId;
    public RoomStatus CurrentStatus => currentStatus;

    protected virtual void Start()
    {
        RegisterWithShip();
        ReportStatus();
    }

    protected virtual void OnDestroy()
    {
        if (MasterShipController.Instance != null)
            MasterShipController.Instance.UnregisterRoom(this);
    }

    protected void RegisterWithShip()
    {
        if (MasterShipController.Instance != null)
            MasterShipController.Instance.RegisterRoom(this);
    }

    protected void ReportStatus()
    {
        if (MasterShipController.Instance != null)
            MasterShipController.Instance.UpdateRoomStatus(roomId, currentStatus);
    }

    protected void SetStatus(RoomStatus newStatus)
    {
        if (currentStatus == newStatus) return;

        currentStatus = newStatus;
        ReportStatus();
    }

    // Optional override points
    public virtual void OnShipPowerRestored() { }
    public virtual void OnShipEmergencyMode() { }
}
