using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatUI : MonoBehaviour
{
    [SerializeField] float warningValue = 0.3f;
    ParticleSystem glowParticle;
    ParticleSystem.EmissionModule glowEmission;

    Slider slider;
    StatTextUI textUI;
    float targetValue;
    bool isLerping;

    // Start is called before the first frame update
    void Awake()
    {
        slider = GetComponent<Slider>();
        textUI = GetComponentInChildren<StatTextUI>(true);
        isLerping = false;
        glowParticle = GetComponentInChildren<ParticleSystem>();
        glowEmission = glowParticle.emission;
    }

    public void SetupStatUI(float warningValue, float startingValue, float maximumValue)
    {
        this.warningValue = warningValue;
        slider.value = startingValue;
        slider.maxValue = maximumValue;
        targetValue = startingValue;
    }

    private void Update()
    {
        if (Mathf.Abs(targetValue - slider.value) > 1f && !isLerping)
        {
            isLerping = true;
            StopCoroutine(LerpToTargetValue());
            StartCoroutine(LerpToTargetValue());
        }

        // Check for critical value
        if (slider.value < warningValue)
        {
            glowEmission.enabled = true;
        }
        else
        {
            glowEmission.enabled = false;
        }
    }

    // Will cause filling
    public void SetTargetValue(float newValue)
    {
        targetValue = newValue;
    }

    // Immediate fill
    public void SetCurrentValue(float newValue)
    {
        slider.value = newValue;
        targetValue = newValue;
    }

    IEnumerator LerpToTargetValue()
    {
        float difference = targetValue - slider.value;
        
        while (Mathf.Abs(targetValue - slider.value) > 1f)
        {
            slider.value += difference * Time.deltaTime * 5.0f;
            yield return null;
        }

        slider.value = targetValue;
        isLerping = false;
    }

    public void DisplayValues(bool isDisplaying)
    {
        textUI.gameObject.SetActive(isDisplaying);
    }
}
