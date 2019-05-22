using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] NecromancerMainMenu necromancer = null;
    [SerializeField] float startDelay = 2.0f;
    [SerializeField] AudioClip menuClickSound = null;
    [SerializeField] AudioClip menuHoverSound = null;
    [SerializeField] GameObject buttons = null;
    [SerializeField] Canvas menuCanvas = null;
    [SerializeField] Canvas creditsCanvas = null;

    Animator buttonAnimator;
    AudioSource audioSource;
    UIFader uiFader;

    


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        buttonAnimator = buttons.GetComponent<Animator>();
        uiFader = GetComponentInChildren<UIFader>();
    }

    public void StartGame()
    {
        // Make Necromancer Stand up
        necromancer.StandUp();

        // Play Sound
        audioSource.PlayOneShot(menuClickSound);

        // Move Menu Down
        buttonAnimator.SetTrigger("MoveDown");

        // Fade out
        Invoke("FadeOut", 5f);

        // Start Game with Delay
        StopCoroutine(WaitSeconds(startDelay));
        StartCoroutine(WaitSeconds(startDelay));
    }

    void FadeOut()
    {
        uiFader.FadeOut();
    }

    public void LoadCredits()
    {
        uiFader.SwitchToCanvas(creditsCanvas);
    }

    public void LoadMenu()
    {
        uiFader.SwitchToCanvas(menuCanvas);
    }

    public void QuitGame()
    {
        // Play Sound
        audioSource.PlayOneShot(menuClickSound);

        Application.Quit();
    }

    IEnumerator WaitSeconds(float seconds)
    {
        print("Waiting for " + seconds + " seconds.");

        yield return new WaitForSeconds(seconds);

        SceneLoader.LoadLevel0();
    }
}
