using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    Canvas mainCanvas;

    public Canvas MainCanvas { get => mainCanvas; }

    // Start is called before the first frame update
    void Awake()
    {
        mainCanvas = GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
