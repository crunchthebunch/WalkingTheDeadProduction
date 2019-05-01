using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayerSingleton : MonoBehaviour
{
    AudioSource backGroundMusic;

    private void Awake()
    {
        int numberOfAudioPlayers = FindObjectsOfType<AudioPlayerSingleton>().Length;

        if (numberOfAudioPlayers > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(this);
        }

        backGroundMusic = GetComponent<AudioSource>();
        backGroundMusic.Play();
    }
}
