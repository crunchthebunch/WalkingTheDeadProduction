using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject book = null;

    private void Awake()
    {
        StopCoroutine(RotateBook());
        StartCoroutine(RotateBook());
    }

    IEnumerator RotateBook()
    {
        while (true)
        {
            Vector3 rotation = transform.rotation.eulerAngles;
            rotation.y += Time.deltaTime * 10f;

            book.transform.Rotate(rotation);

            yield return null;
        }
    }

    public void StartGame()
    {
        SceneLoader.LoadTutorial();
    }

    public void LoadCredits()
    {
        SceneLoader.LoadCredits();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
