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
    Animator buttonAnimator;
    AudioSource audioSource;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        buttonAnimator = buttons.GetComponent<Animator>();  
    }

    public void StartGame()
    {
        // Make Necromancer Stand up
        necromancer.StandUp(5.0f);

        // Play Sound
        audioSource.PlayOneShot(menuClickSound);

        // Move Menu Down
        buttonAnimator.SetTrigger("MoveDown");

        // Start Game with Delay
        StopCoroutine(WaitSeconds(startDelay));
        StartCoroutine(WaitSeconds(startDelay));
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
