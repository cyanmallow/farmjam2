using UnityEngine;

[ExecuteAlways]
public class LightingManager : MonoBehaviour
{
    public static LightingManager Instance { get; private set; }

    [SerializeField] private Light DirectionalLight;
    [SerializeField] private LightingPresets Preset;

    //variables for material properties
    [SerializeField, Range(0, 24)] public float TimeOfDay;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (TimeOfDay >= 24f)
        {
            TimeOfDay %= 24f; // wrap, don't just clamp/reset to 0 — preserves overflow (e.g. 24.3 -> 0.3)
        }
        UpdateLighting(TimeOfDay / 24f);
    }
    private void UpdateLighting(float timePercent)
    {
        if (Preset == null) return;
        RenderSettings.ambientLight = Preset.ambientColor.Evaluate(timePercent);
        RenderSettings.fogColor = Preset.fogColor.Evaluate(timePercent);

        if (DirectionalLight != null)
        {
            DirectionalLight.color = Preset.directionalLightColor.Evaluate(timePercent);
            DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0));
        }
    }

    private void OnValidate()
    {
        if (DirectionalLight == null)
        {
            if (RenderSettings.sun != null)
            {
                DirectionalLight = RenderSettings.sun;
            }
            else
            {
                Light[] lights = FindObjectsByType<Light>(FindObjectsSortMode.None);
                foreach (Light light in lights)
                {
                    if (light.type == LightType.Directional)
                    {
                        DirectionalLight = light;
                        break;
                    }
                }
            }
        }

        if (DirectionalLight != null)
        {
            DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((TimeOfDay / 24f) * 360f - 90f, 170f, 0));
        }
    }
}
