using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class EnvironmentManager : MonoBehaviour
{
    [Header("Emergency Lights")]
    [SerializeField] private Light2D[] emergencyLights;

    [Header("Main Light")]
    [SerializeField] private Light2D mainLight;

    [Header("Fade Settings")]
    [SerializeField] private float targetIntensity = 1f;
    [SerializeField] private float fadeDuration = 0.5f;

    private Coroutine fadeRoutine;

    private void Start()
    {
        // Emergency lights ON
        foreach (var light in emergencyLights)
        {
            if (light != null)
            {
                light.enabled = true;
                light.intensity = targetIntensity;
            }
        }

        // Main light OFF
        if (mainLight != null)
        {
            mainLight.enabled = true;
            mainLight.intensity = 0f;
        }

        // Subscribe to power event
        ControlRoomManager.Instance.OnMainPowerRestored += HandleMainPowerRestored;
    }

    private void OnDestroy()
    {
        if (ControlRoomManager.Instance != null)
            ControlRoomManager.Instance.OnMainPowerRestored -= HandleMainPowerRestored;
    }

    private void HandleMainPowerRestored()
    {
        // Turn off emergency lights immediately
        foreach (var light in emergencyLights)
        {
            if (light != null)
            {
                light.intensity = 0f;
                light.enabled = false;
            }
        }

        // Fade in main light
        if (fadeRoutine != null)
            StopCoroutine(fadeRoutine);

        fadeRoutine = StartCoroutine(FadeMainLight());
    }

    private IEnumerator FadeMainLight()
    {
        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float t = time / fadeDuration;

            if (mainLight != null)
                mainLight.intensity = Mathf.Lerp(0f, targetIntensity, t);

            yield return null;
        }

        if (mainLight != null)
            mainLight.intensity = targetIntensity;
    }
}
