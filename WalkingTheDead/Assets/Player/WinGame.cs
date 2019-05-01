using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinGame : MonoBehaviour
{
    string tagToCheckFor = "Necromancer";
    LoadSceneOnClick sceneLoader;

    private void Awake()
    {
        sceneLoader = FindObjectOfType<LoadSceneOnClick>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(tagToCheckFor))
        {
            sceneLoader.LoadWinScreen();
        }
    }
}
