using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatUI : MonoBehaviour
{
    [SerializeField] float warningValue = 0.3f;

    Slider slider;
    StatTextUI textUI;
    

    // Start is called before the first frame update
    void Awake()
    {
        slider = GetComponent<Slider>();
        textUI = GetComponentInChildren<StatTextUI>(true);
    }

    private void Update()
    {
        CheckForGlow();
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

        // CheckForGlow();
    }

    private void CheckForGlow()
    {
    }

    public void DisplayValues(bool isDisplaying)
    {
        textUI.gameObject.SetActive(isDisplaying);
    }
}
