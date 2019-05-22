using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{

    [SerializeField] GameObject spellDescriptionpanel;
    [SerializeField] Canvas winScreen = null;
    [SerializeField] Canvas loseScreen = null;

    PauseMenu pauseMenu;
    UIFader fader;

    private void Awake()
    {
        pauseMenu = GetComponentInChildren<PauseMenu>();
        fader = FindObjectOfType<UIFader>();
    }

    private void Update()
    {
        // Escape - Pause Menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
        // M - Show Map
        if (Input.GetKeyDown(KeyCode.M))
        {
            ShowMap();
        }
    }

    public void ShowSpellDescription(UISpell spell)
    {
        spellDescriptionpanel.SetActive(true);

        spell.HoverSpell();

        // Update the spell Text
        spellDescriptionpanel.GetComponentInChildren<TextMeshProUGUI>().text = spell.SpellDescription;
    }

    public void HideSpellDescription(UISpell spell)
    {
        spellDescriptionpanel.SetActive(false);

        spell.StopHoveringSpell();
    }

    public void ShowStatValue(UIStat stat)
    {
        // Make the value text visible
        stat.DisplayValues(true);
    }

    public void HideStatValue(UIStat stat)
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

    public void FadeInWinScreen()
    {
        fader.SwitchToCanvas(winScreen);
    }

    public void FadeInLoseScreen()
    {
        fader.SwitchToCanvas(loseScreen);
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

    public void LoadMainMenu()
    {
        fader.FadeOut();
        Time.timeScale = 1.0f;

        Invoke("LoadMenuScene", 1f);
    }

    public void LoadNextLevel()
    {
        fader.FadeOut();
        Time.timeScale = 1.0f;

        Invoke("LoadNextScene", 1f);
    }

    void LoadMenuScene()
    {
        SceneLoader.LoadMainMenu();
    }

    void LoadNextScene()
    {
        SceneLoader.LoadLevel1();
    }
}
