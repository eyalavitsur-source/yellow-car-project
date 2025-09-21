using UnityEngine;
using System.Collections;

public class LampFlicker : MonoBehaviour
{
    private Light lamp;
    private float intensity;

    void Awake()
    {
        lamp = GetComponent<Light>();
        if (lamp != null)
            intensity = lamp.intensity; // save original intensity
    }

    public void StartBlinking(float interval = 1f)
    {
        if (lamp != null)
            StartCoroutine(BlinkRoutine(interval));
    }

    IEnumerator BlinkRoutine(float interval)
    {
        while (true)
        {
            lamp.enabled = !lamp.enabled;   // toggle on/off
            yield return new WaitForSeconds(interval);
        }
    }
}