using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromancerMainMenu : MonoBehaviour
{
    Animator animator;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void StandUp(float seconds)
    {
        animator.SetTrigger("StartPressed");
    }

    
    
}
