using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    static public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    static public void LoadLevel0()
    {
        SceneManager.LoadScene("Level0");
    }

    static public void LoadLevel1()
    {
        SceneManager.LoadScene("Level1");
    }
}
