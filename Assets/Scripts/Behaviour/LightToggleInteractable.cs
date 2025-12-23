using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class LightToggleInteractable : MonoBehaviour, IInteractable
{
    [Header("Lights to Control")]
    [SerializeField] private Light2D[] lights;

    [Header("Fade Settings")]
    [SerializeField] private float targetIntensity = 1f;
    [SerializeField] private float fadeDuration = 0.5f;

    [Header("Optional Visual Feedback")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Color onColor = Color.green;
    [SerializeField] private Color offColor = Color.red;

    private bool isOn = true;
    private Coroutine fadeRoutine;

    private void Awake()
    {
       

        foreach (var light in lights)
        {
            if (light != null)
            {
                light.enabled = true;
                light.intensity = isOn ? targetIntensity : 0f;
            }
        }

        UpdateVisual();
    }

    public void Interact(PlayerStateController player)
    {
        isOn = !isOn;

        if (fadeRoutine != null)
            StopCoroutine(fadeRoutine);

        fadeRoutine = StartCoroutine(FadeLights(isOn));
        UpdateVisual();
    }

    private IEnumerator FadeLights(bool turnOn)
    {
        float start = turnOn ? 0f : targetIntensity;
        float end = turnOn ? targetIntensity : 0f;
        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float t = time / fadeDuration;

            float intensity = Mathf.Lerp(start, end, t);

            foreach (var light in lights)
            {
                if (light != null)
                    light.intensity = intensity;
            }

            yield return null;
        }

        foreach (var light in lights)
        {
            if (light != null)
                light.intensity = end;
        }
    }

    private void UpdateVisual()
    {
        if (spriteRenderer != null)
            spriteRenderer.color = isOn ? onColor : offColor;
    }

    public void Focus() { }
    public void Unfocus() { }
}
