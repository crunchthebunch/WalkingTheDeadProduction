using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromancerMainMenu : MonoBehaviour
{
    Animator animator;
    AudioSource audioSource;

    void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void StandUp()
    {
        animator.SetTrigger("StartPressed");
    }

    public void PlayBookSound()
    {
        audioSource.Play();
    }
    
    
}
