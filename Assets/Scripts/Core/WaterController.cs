using UnityEngine;

namespace RaahiFishing.Core
{
    /// <summary>
    /// Controls water appearance and animation
    /// Provides easy inspector controls for water shader properties
    /// </summary>
    [RequireComponent(typeof(MeshRenderer))]
    public class WaterController : MonoBehaviour
    {
        [Header("Water Colors")]
        [SerializeField] private Color waterColor = new Color(0f, 0.47f, 0.75f, 0.8f);
        [SerializeField] private Color deepWaterColor = new Color(0f, 0.2f, 0.4f, 1f);
        [SerializeField] private Color fresnelColor = new Color(0.7f, 0.9f, 1f, 1f);

        [Header("Wave Settings")]
        [SerializeField] [Range(0f, 2f)] private float waveSpeed = 0.5f;
        [SerializeField] [Range(0f, 0.5f)] private float waveAmplitude = 0.1f;
        [SerializeField] [Range(0f, 10f)] private float waveFrequency = 2f;

        [Header("Surface Properties")]
        [SerializeField] [Range(0f, 1f)] private float smoothness = 0.8f;
        [SerializeField] [Range(0f, 5f)] private float fresnelPower = 2f;

        private Material waterMaterial;
        private MeshRenderer meshRenderer;

        private void Awake()
        {
            meshRenderer = GetComponent<MeshRenderer>();
            
            // Create a unique material instance
            if (meshRenderer.sharedMaterial != null)
            {
                waterMaterial = new Material(meshRenderer.sharedMaterial);
                meshRenderer.material = waterMaterial;
            }
        }

        private void Start()
        {
            ApplyWaterSettings();
        }

        private void OnValidate()
        {
            // Update in editor when values change
            if (Application.isPlaying && waterMaterial != null)
            {
                ApplyWaterSettings();
            }
        }

        private void ApplyWaterSettings()
        {
            if (waterMaterial == null) return;

            waterMaterial.SetColor("_WaterColor", waterColor);
            waterMaterial.SetColor("_DeepWaterColor", deepWaterColor);
            waterMaterial.SetColor("_FresnelColor", fresnelColor);
            waterMaterial.SetFloat("_WaveSpeed", waveSpeed);
            waterMaterial.SetFloat("_WaveAmplitude", waveAmplitude);
            waterMaterial.SetFloat("_WaveFrequency", waveFrequency);
            waterMaterial.SetFloat("_Glossiness", smoothness);
            waterMaterial.SetFloat("_FresnelPower", fresnelPower);
        }

        /// <summary>
        /// Change water color at runtime (e.g., for day/night cycle)
        /// </summary>
        public void SetWaterColor(Color color)
        {
            waterColor = color;
            ApplyWaterSettings();
        }

        /// <summary>
        /// Change wave intensity at runtime (e.g., for weather effects)
        /// </summary>
        public void SetWaveIntensity(float amplitude, float speed)
        {
            waveAmplitude = Mathf.Clamp(amplitude, 0f, 0.5f);
            waveSpeed = Mathf.Clamp(speed, 0f, 2f);
            ApplyWaterSettings();
        }

        private void OnDestroy()
        {
            // Clean up material instance
            if (waterMaterial != null)
            {
                Destroy(waterMaterial);
            }
        }
    }
}
