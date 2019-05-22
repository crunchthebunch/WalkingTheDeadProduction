using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIStatText : MonoBehaviour
{
    Slider slider;
    TextMeshProUGUI text;

    private void Awake()
    {
        slider = GetComponentInParent<Slider>();
        text = GetComponentInChildren<TextMeshProUGUI>(true);
    }

    private void OnEnable()
    {
        AdjustValue();
    }

    public void AdjustValue()
    {
        text.text = (int)slider.value + "/" + (int)slider.maxValue;
    }
}
