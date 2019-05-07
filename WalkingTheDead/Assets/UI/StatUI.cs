using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatUI : MonoBehaviour
{
    Slider slider;
    ParticleSystem glowEffect;
    [SerializeField] float triggerValue = 0.3f;

    // Start is called before the first frame update
    void Awake()
    {
        slider = GetComponent<Slider>();
        glowEffect = GetComponentInChildren<ParticleSystem>();

        glowEffect.Pause(); // TODO Fix
    }

    // Update is called once per frame
    void Update()
    {
        // If the value is under the trigger value
        if (slider.value / slider.maxValue < triggerValue)
        {
            
            // Play Glow if it's not being played
            if (!glowEffect.isPlaying)
            {
                print("Start Playing");
                glowEffect.Play();
            }
        }
        else
        {
            // Disable Emission
            if (glowEffect.isPlaying) { glowEffect.Pause(); }
        }
    }

    public void SetupStatUI(float currentValue, float maximumValue)
    {
        slider.value = currentValue;
        slider.value = maximumValue;
    }

    public void SetCurrentValue(float newValue)
    {
        slider.value = newValue;

        //// If the value is under the trigger value
        //if (slider.value / slider.maxValue < triggerValue)
        //{
        //    // Play Glow if it's not being played
        //    if (!glowEffect.isPlaying) { glowEffect.Play(); }
        //}
    }
}
