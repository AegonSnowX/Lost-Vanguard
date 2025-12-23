using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BridgeRoomController : RoomController
{
    [Header("Lights")]
    [SerializeField] private GameObject[] emergencyLights;
    [SerializeField] private Light2D mainLight;

    protected override void Start()
    {
        base.Start();
        EnterEmergencyMode();
        SetStatus(RoomStatus.Critical);
    }

    public void RestoreLocalPower()
    {
        ExitEmergencyMode();
        SetStatus(RoomStatus.Normal);

        // Inform ship-wide system
        MasterShipController.Instance.RequestMainPowerRestored();
    }

    private void EnterEmergencyMode()
    {
        foreach (var light in emergencyLights)
            if (light != null) light.SetActive( true);

        if (mainLight != null)
            mainLight.intensity = 0f;
    }

    private void ExitEmergencyMode()
    {
        foreach (var light in emergencyLights)
            if (light != null) light.SetActive (false);

        if (mainLight != null)
            mainLight.intensity = 1f;
    }
}
