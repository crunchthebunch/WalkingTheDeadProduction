using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatUI : MonoBehaviour
{
    Slider slider;
    ParticleSystem.EmissionModule emissionModule;
    [SerializeField] float warningValue = 0.3f;

    // Start is called before the first frame update
    void Awake()
    {
        slider = GetComponent<Slider>();
        emissionModule = GetComponentInChildren<ParticleSystem>().emission;
    }

    public void SetupStatUI(float warningValue, float startingValue, float maximumValue)
    {
        this.warningValue = warningValue;
        slider.value = startingValue;
        slider.maxValue = maximumValue;
    }

    public void SetCurrentValue(float newValue)
    {
        slider.value = newValue;

        // If the value is under the trigger value
        if (slider.value / slider.maxValue < warningValue)
        {
            emissionModule.enabled = true;
        }
        else
        {
            // Disable Emission
            emissionModule.enabled = false;
        }
    }
}
