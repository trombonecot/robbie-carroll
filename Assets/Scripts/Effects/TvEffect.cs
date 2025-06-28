using UnityEngine;

[RequireComponent(typeof(Light))]
public class TVEffect : MonoBehaviour
{
    private Light tvLight;

    [Header("Intensity Flicker Settings")]
    public float minIntensity = 0.5f;
    public float maxIntensity = 1.5f;
    public float flickerSpeed = 0.1f;

    [Header("Color Flicker Settings")]
    public Color[] flickerColors;
    public float colorChangeSpeed = 0.3f;

    private float nextIntensityChangeTime;
    private float nextColorChangeTime;

    void Start()
    {
        tvLight = GetComponent<Light>();

        if (flickerColors == null || flickerColors.Length == 0)
        {
            // Default color range to simulate a TV glow
            flickerColors = new Color[]
            {
                new Color(0.4f, 0.4f, 1f),  // bluish
                new Color(0.3f, 0.5f, 1f),  // soft blue
                new Color(0.5f, 0.5f, 0.8f) // cool white
            };
        }
    }

    void Update()
    {
        // Randomize intensity over time
        if (Time.time >= nextIntensityChangeTime)
        {
            tvLight.intensity = Random.Range(minIntensity, maxIntensity);
            nextIntensityChangeTime = Time.time + Random.Range(flickerSpeed * 0.5f, flickerSpeed * 1.5f);
        }

        // Randomize color over time
        if (Time.time >= nextColorChangeTime)
        {
            tvLight.color = flickerColors[Random.Range(0, flickerColors.Length)];
            nextColorChangeTime = Time.time + Random.Range(colorChangeSpeed * 0.5f, colorChangeSpeed * 1.5f);
        }
    }
}
