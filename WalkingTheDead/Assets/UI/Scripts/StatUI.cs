using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatUI : MonoBehaviour
{
    [SerializeField] float warningValue = 0.3f;

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
            print("Change Initiated");
            isLerping = true;
            StopCoroutine(LerpToTargetValue());
            StartCoroutine(LerpToTargetValue());
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
            print("Changing");
        }

        slider.value = targetValue;
        isLerping = false;
        print("Value Reached");
    }

    public void DisplayValues(bool isDisplaying)
    {
        textUI.gameObject.SetActive(isDisplaying);
    }
}
