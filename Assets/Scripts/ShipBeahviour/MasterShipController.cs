using UnityEngine;
using System.Collections.Generic;
using System;

public class MasterShipController : MonoBehaviour
{
    public static MasterShipController Instance;

    private readonly Dictionary<string, RoomStatus> roomStatuses = new();
    private readonly List<RoomController> registeredRooms = new();
      public event Action OnShipStatusChanged;

    private void Awake()
    {
        Instance = this;
    }

    // Registration
    public void RegisterRoom(RoomController room)
    {
        if (room == null) return;

        roomStatuses.Remove(room.RoomId);
        OnShipStatusChanged?.Invoke();
    }

    public void UnregisterRoom(RoomController room)
    {
       if (room == null) return;

        roomStatuses.Remove(room.RoomId);
        OnShipStatusChanged?.Invoke();
    }

    // Reporting (rooms → ship)
    public void UpdateRoomStatus(string roomId, RoomStatus status)
    {
        roomStatuses[roomId] = status;
        OnShipStatusChanged?.Invoke();
        Debug.Log($"[SHIP] Room {roomId} status: {status}");
    }

    // Bridge queries
    public RoomStatus GetRoomStatus(string roomId)
    {
        return roomStatuses.TryGetValue(roomId, out var status)
            ? status
            : RoomStatus.Offline;
    }

    public IReadOnlyDictionary<string, RoomStatus> GetAllRoomStatuses()
    {
        return roomStatuses;
    }

    // Ship-wide requests (bridge → rooms)
    public void RequestMainPowerRestored()
    {
        foreach (var room in registeredRooms)
            room.OnShipPowerRestored();
    }

    public void RequestEmergencyMode()
    {
        foreach (var room in registeredRooms)
            room.OnShipEmergencyMode();
    }
}
