using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    public CinemachineVirtualCamera acCam;

    public static CameraShake Instance { get; private set; }

    void Start()
    {
        Instance = this;
        //acCam = GameObject.FindGameObjectWithTag("Cinemachine").GetComponent<CinemachineVirtualCamera>();
    }

    void Update()
    {
        
    }

    public void ShakeCamera(float intensity, float time)
    {
        StartCoroutine(ShakeCameraCoroutine(intensity, time));
    }

    public IEnumerator ShakeCameraCoroutine(float intensity, float time)
    {
        //acCam = GameObject.Find("Cameras/").GetComponent<CinemachineVirtualCamera>();
        //acCam = GameObject.FindGameObjectWithTag("Cinemachine").GetComponent<CinemachineVirtualCamera>();

        CinemachineBasicMultiChannelPerlin channel;
        channel = acCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        channel.m_AmplitudeGain = intensity;
        channel.m_FrequencyGain = intensity;

        yield return new WaitForSecondsRealtime(time);

        channel = acCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        channel.m_AmplitudeGain = 0f;
        channel.m_FrequencyGain = 0f;
    }
}
