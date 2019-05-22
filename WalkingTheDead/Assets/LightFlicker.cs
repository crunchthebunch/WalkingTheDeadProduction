using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    Light lightSource;
    bool isIncreasing;

    // Start is called before the first frame update
    void Awake()
    {
        lightSource = GetComponent<Light>();
        isIncreasing = false;
    }

    private void Start()
    {
        StartCoroutine(LowerIntensity());
    }


    IEnumerator IncreaseIntensity()
    {
        while (lightSource.intensity < 1.5f)
        {
            lightSource.intensity += Time.deltaTime;
            yield return null;
        }

        StopCoroutine(LowerIntensity());
        StartCoroutine(LowerIntensity());
        
    }

    IEnumerator LowerIntensity()
    {
        while (lightSource.intensity > 0.8f)
        {
            lightSource.intensity -= Time.deltaTime;
            yield return null;
        }

        StopCoroutine(IncreaseIntensity());
        StartCoroutine(IncreaseIntensity());
    }
}
