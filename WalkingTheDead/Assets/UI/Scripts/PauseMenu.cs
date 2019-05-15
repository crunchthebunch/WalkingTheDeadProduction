using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    Canvas parentCanvas;

    [SerializeField] Canvas mainCanvas;
    [SerializeField] Canvas controlsCanvas;
    [SerializeField] Canvas mapCanvas;

    [SerializeField] AudioClip pageTurnSound;

    AudioSource audioSource;
    

    public Canvas ParentCanvas { get => parentCanvas; }
    public Canvas ControlsCanvas { get => controlsCanvas; }
    public Canvas MapCanvas { get => mapCanvas; }
    public Canvas MainCanvas { get => mainCanvas; }

    // Start is called before the first frame update
    void Awake()
    {
        parentCanvas = GetComponent<Canvas>();
        audioSource = GetComponent<AudioSource>();
    }

    public void ShowMain()
    {
        mainCanvas.enabled = true;
        mapCanvas.enabled = false;
        controlsCanvas.enabled = false;
        audioSource.PlayOneShot(pageTurnSound);
    }

    public void ShowMap()
    {
        mapCanvas.enabled = true;
        mainCanvas.enabled = false;
        controlsCanvas.enabled = false;
        audioSource.PlayOneShot(pageTurnSound);
    }

    public void ShowControls()
    {
        controlsCanvas.enabled = true;
        mainCanvas.enabled = false;
        mapCanvas.enabled = false;
        audioSource.PlayOneShot(pageTurnSound);
    }

    public void LoadMainMenu()
    {
        SceneLoader.LoadMainMenu();
    }

    public void DisplayPauseMenu(bool isDisplaying)
    {
        parentCanvas.enabled = isDisplaying;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
