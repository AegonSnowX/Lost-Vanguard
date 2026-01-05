using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class AirLockRoomController : RoomController , IRoomPowerControllable 
{
 [Header("Lights")]
    [SerializeField] private GameObject[] emergencyLights;
    [SerializeField] private Light2D mainLight;

    [Header("Fade Settings")]
    [SerializeField] private float mainLightTargetIntensity = 1f;
    [SerializeField] private float fadeDuration = 0.8f;

    private Coroutine powerRoutine;

    protected override void Start()
    {
        base.Start();
        EnterEmergencyMode();
        SetStatus(RoomStatus.Critical);
    }

    public void RestoreLocalPower()
    {
        if (powerRoutine != null)
            StopCoroutine(powerRoutine);

        powerRoutine = StartCoroutine(RestorePowerSequence());

        // Inform ship-wide system immediately (intent is declared)
        MasterShipController.Instance.RequestMainPowerRestored();
    }

    private void EnterEmergencyMode()
    {
        foreach (var light in emergencyLights)
            if (light != null)
                light.SetActive(true);

        if (mainLight != null)
        {
            mainLight.enabled = true;
            mainLight.intensity = 0f;
        }
    }

    private IEnumerator RestorePowerSequence()
    {
        // Fade main light in
        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float t = time / fadeDuration;

            if (mainLight != null)
                mainLight.intensity = Mathf.Lerp(0f, mainLightTargetIntensity, t);

            yield return null;
        }

        if (mainLight != null)
            mainLight.intensity = mainLightTargetIntensity;

        // Now turn off emergency lights
        foreach (var light in emergencyLights)
            if (light != null)
                light.SetActive(false);

        // Room is now stable
        SetStatus(RoomStatus.Normal);
    }
}

