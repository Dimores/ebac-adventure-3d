using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;
using Cinemachine;

public class ShakeCamera : Singleton<ShakeCamera>
{
    public CinemachineVirtualCamera virtualCamera;

    public float shakeTime;

    [Header("Shake Settings")]
    public float frequency = 3f;
    public float amplitude = 3f;
    public float time = .2f;


    private CinemachineBasicMultiChannelPerlin perlin;

    private void Start()
    {
        perlin = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void Shake()
    {
        Shake(amplitude, frequency, time);
    }

    public void Shake(float amplitude, float frequency, float time)
    {
        perlin.m_AmplitudeGain = amplitude;
        perlin.m_FrequencyGain = frequency;

        shakeTime = time;
    }

    private void Update()
    {
        if (shakeTime > 0)
        {
            shakeTime -= Time.deltaTime;
        }
        else
        {
            perlin.m_AmplitudeGain = 0f;
            perlin.m_FrequencyGain = 0f;
        }
    }
}
