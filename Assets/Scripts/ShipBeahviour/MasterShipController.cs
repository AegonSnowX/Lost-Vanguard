using UnityEngine;
using System.Collections.Generic;

public class MasterShipController : MonoBehaviour
{
    public static MasterShipController Instance;

    private readonly Dictionary<string, RoomStatus> roomStatuses = new();
    private readonly List<RoomController> registeredRooms = new();

    private void Awake()
    {
        Instance = this;
    }

    // Registration
    public void RegisterRoom(RoomController room)
    {
        if (room == null || registeredRooms.Contains(room)) return;

        registeredRooms.Add(room);
        roomStatuses[room.RoomId] = room.CurrentStatus;
    }

    public void UnregisterRoom(RoomController room)
    {
        if (room == null) return;

        registeredRooms.Remove(room);
        roomStatuses.Remove(room.RoomId);
    }

    // Reporting (rooms → ship)
    public void UpdateRoomStatus(string roomId, RoomStatus status)
    {
        roomStatuses[roomId] = status;
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
