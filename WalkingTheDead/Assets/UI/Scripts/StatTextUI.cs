using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatTextUI : MonoBehaviour
{
    Slider slider;
    TextMeshProUGUI text;

    private void Awake()
    {
        slider = GetComponentInParent<Slider>();
        text = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        text.text = (int)slider.value + "/" + (int)slider.maxValue;
    }

    

}
