using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    static public void LoadCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    static public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    static public void LoadTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    static public void LoadLevel1()
    {
        SceneManager.LoadScene("Level1");
    }

    static public void LoadLevel2()
    {
        SceneManager.LoadScene("Level2");
    }
}
