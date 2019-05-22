using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFader : MonoBehaviour
{
    [SerializeField] Canvas currentCanvas = null;

    Animator animator;
    Canvas canvasToLoad;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SwitchToCanvas(Canvas canvasToLoad)
    {
        this.canvasToLoad = canvasToLoad;

        StopCoroutine(FadeToCanvas());
        StartCoroutine(FadeToCanvas());
    }

    IEnumerator FadeToCanvas()
    {
        FadeOut();

        yield return new WaitForSeconds(1f);

        // Only display one canvas
        currentCanvas.enabled = false;
        canvasToLoad.enabled = true;
        currentCanvas = canvasToLoad;

        FadeIn();
    }

    public void FadeOut()
    {
        animator.SetTrigger("FadeOut");
    }

    public void FadeIn()
    {
        animator.SetTrigger("FadeIn");
    }
}
