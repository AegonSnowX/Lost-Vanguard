using UnityEngine;

public class ElectricalInteractable : MonoBehaviour,IInteractable
{

    [SerializeField] private BridgeRoomController bridgeRoom;

    public void Interact(PlayerStateController player)
    {
        bridgeRoom.RestoreLocalPower();
    }

    public void Focus() { }
    public void Unfocus() { }
}
