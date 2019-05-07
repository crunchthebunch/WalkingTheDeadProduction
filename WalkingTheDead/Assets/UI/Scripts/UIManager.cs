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

        spell.HoverSpell();

        // Update the spell Text
        spellDescription.GetComponentInChildren<TextMeshProUGUI>().text = spell.SpellDescription;
    }

    public void HideSpellDescription(SpellUI spell)
    {
        spellDescription.SetActive(false);

        spell.StopHoveringSpell();
    }

    public void ShowStatValue(StatUI stat)
    {
        // Make the value text visible
        stat.DisplayValues(true);
    }

    public void HideStatValue(StatUI stat)
    {
        // Make value invisible
        stat.DisplayValues(false);
    }
}
