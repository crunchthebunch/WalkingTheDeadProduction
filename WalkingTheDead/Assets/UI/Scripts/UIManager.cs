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
        // Escape - Pause Menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
        // M - Show Map
        else if (Input.GetKeyDown(KeyCode.M))
        {
            ShowMap();
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
        // If already Paused
        if (pauseMenu.ParentCanvas.enabled)
        {
            // Return to Game
            pauseMenu.DisplayPauseMenu(false);
            Time.timeScale = 1.0f;
        }
        else
        {
            // Show main screen
            pauseMenu.DisplayPauseMenu(true);
            pauseMenu.ShowMain();
            Time.timeScale = 0.0f;
        }
    }

    public void ShowMap()
    {
        // Allow hotkey in game mode (not in pause mode)
        if (!pauseMenu.ParentCanvas.enabled)
        {
            pauseMenu.DisplayPauseMenu(true);
            pauseMenu.ShowMap();
            Time.timeScale = 0.0f;
        }
    }
}
