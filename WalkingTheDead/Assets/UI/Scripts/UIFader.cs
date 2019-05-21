using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFader : MonoBehaviour
{
    // Get the image component
    Image background;
    Color backgroundColor;
    Canvas canvasToLoad;

    // Start is called before the first frame update
    void Awake()
    {
        background = GetComponent<Image>();
        backgroundColor = background.color;
    }

    public void LoadEndScreen(Canvas canvasToLoad)
    {
        this.canvasToLoad = canvasToLoad;

        StopCoroutine(FadeOut());
        StartCoroutine(FadeOut());
    }

    public void LoadMainMenu()
    {
        SceneLoader.LoadMainMenu();
    }

    IEnumerator FadeOut()
    {
        while (background.color.a < 1f)
        {
            backgroundColor.a += Time.deltaTime;
            background.color = backgroundColor;
            yield return null;
        }

        canvasToLoad.enabled = true;

        StopCoroutine(FadeIn());
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        while (background.color.a > 0f)
        {
            backgroundColor.a -= Time.deltaTime;
            background.color = backgroundColor;
            yield return null;
        }
    }
}
