using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundScript : MonoBehaviour
{
    [SerializeField] AudioClip[] deathClips;
    [SerializeField] AudioClip[] idleClips;
    [SerializeField] AudioClip[] takeDamageClips;
    [SerializeField] AudioClip[] screamClips;
    [SerializeField] AudioClip[] raiseClips;
    [SerializeField] AudioClip[] attackClips;
    AudioSource audioSource;

    float idleTimer = 0;
    float idleMin = 20;
    float idleMax = 60;

    float attackTimer = 0;
    float attackMax = 4;

    float screamTimer = 0;
    float screamMax = 10;
    float screamMin = 5;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        idleTimer = Random.Range(5, idleMin);
    }

    private void Update()
    {
        if(idleClips.Length > 0) PlayIdleClip();
        CountdownTimers();
    }
    void PlayClip(AudioClip[] clips)
    {
        if (audioSource.isPlaying) return;
        if (clips.Length <= 0) return;

        audioSource.clip = clips[Random.Range(0, clips.Length)];
        audioSource.Play();
    }
    void CountdownTimers()
    {
        if(attackTimer >= 0) attackTimer -= Time.deltaTime;
        if(idleTimer >= 0) idleTimer -= Time.deltaTime;
        if (screamTimer >= 0) screamTimer -= Time.deltaTime;
    }

    public void PlayDeathSound()
    {
        PlayClip(deathClips);
    }
    public void PlayIdleClip()
    {
        if (idleTimer <= 0)
        {
            PlayClip(idleClips);
            idleTimer = Random.Range(idleMin, idleMax);
        }
    }
    public void PlayDamageClip()
    {
            PlayClip(takeDamageClips);
            
    }
    public void PlayScreamClip()
    {
        if (screamTimer <= 0)
        {
            PlayClip(screamClips);
            screamTimer = Random.Range(screamMin, screamMax);
        }
    }
    public void PlayRaiseClip()
    {
        PlayClip(raiseClips);
    }
    public void PlayAttackClip()
    {

        if (attackTimer <= 0)
        {
            PlayClip(attackClips);
            attackTimer = attackMax;
        }
    }
}
