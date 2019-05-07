using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{

    [SerializeField] GameObject spellDescription;

    public void ShowSpellDescription(SpellUI spell)
    {
        spellDescription.SetActive(true);

        // Update the spell Text
        spellDescription.GetComponentInChildren<TextMeshProUGUI>().text = spell.SpellDescription;
    }

    public void HideSpellDescription()
    {
        spellDescription.SetActive(false);
    }

    public void ShowStatValue(StatTextUI stat)
    {
        // Make the value text visible
        stat.gameObject.SetActive(true);
    }

    public void HideStatValue(StatTextUI stat)
    {
        // Make value invisible
        stat.gameObject.SetActive(false);
    }
}
