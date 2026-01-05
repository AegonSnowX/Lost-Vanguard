using UnityEngine;

public class ElectricalInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private MonoBehaviour roomTarget;

    private IRoomPowerControllable powerTarget;

    private void Awake()
    {
        powerTarget = roomTarget as IRoomPowerControllable;

        if (powerTarget == null)
        {
            Debug.LogError(
                $"{name}: Assigned target does not implement IRoomPowerControllable",
                this
            );
        }
    }

    public void Interact(PlayerStateController player)
    {
        powerTarget?.RestoreLocalPower();
        Debug.Log(powerTarget + " interacted" + roomTarget);
    }

    public void Focus() { }
    public void Unfocus() { }
}
