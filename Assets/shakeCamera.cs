using UnityEngine;
using Cinemachine;
public class ScreenShake : MonoBehaviour
{
    [SerializeField] private float shakeDuration = 0.3f; // How long the shake lasts
    [SerializeField] private float shakeAmplitude = 1.0f; // Intensity of the shake
    [SerializeField] private float shakeFrequency = 2.0f; // Speed of the shake

    private CinemachineVirtualCamera virtualCamera; // Reference to the virtual camera
    private CinemachineBasicMultiChannelPerlin noise; // Noise component for shaking
    private float shakeTimer; // Tracks remaining shake time

    void Awake()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        if (noise == null) Debug.LogError("Cinemachine Virtual Camera is missing the Basic Multi Channel Perlin component!");
    }

    void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;

            if (shakeTimer <= 0)
            {
                noise.m_AmplitudeGain = 0f;
                noise.m_FrequencyGain = 0f;
            }
        }
    }
    public void TriggerShake()
    {
        if (noise != null)
        {
            noise.m_AmplitudeGain = shakeAmplitude;
            noise.m_FrequencyGain = shakeFrequency;
            shakeTimer = shakeDuration;
        }
    }
}