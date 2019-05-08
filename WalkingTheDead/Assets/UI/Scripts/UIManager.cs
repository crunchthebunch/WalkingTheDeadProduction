using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{

    [SerializeField] GameObject spellDescription;
    PauseMenu pauseMenu;

    private void Awake()
    {
        pauseMenu = GetComponentInChildren<PauseMenu>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }

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

    public void TogglePauseMenu()
    {
        // Show or Hide the Pause Menu
        if (pauseMenu.MainCanvas.enabled)
        {
            pauseMenu.MainCanvas.enabled = false;
        }
        else
        {
            pauseMenu.MainCanvas.enabled = true;
        }

        
    }
}
