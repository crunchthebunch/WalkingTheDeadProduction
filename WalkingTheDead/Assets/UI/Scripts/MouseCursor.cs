using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor : MonoBehaviour
{
    [SerializeField] Sprite cursorSpriteCalculation;
    [SerializeField] Texture2D standardCursor;
    [SerializeField] Texture2D clickedCursor;
    [SerializeField] CursorMode cursorMode = CursorMode.Auto;
    [SerializeField] Vector2 offSet = Vector2.zero;

    private void Awake()
    {
        offSet = cursorSpriteCalculation.pivot; // TODO Change to actual math instead of magic numbers
        offSet.y = 100f;
        SetMouseTextureToStandard();
    }

    // Update is called once per frame
    void Update()
    {
        // TODO MAKE THIS DIFFERENT
        // If left is clicked
        if (Input.GetMouseButtonDown(0))
        {
            SetMouseTextureToClick();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            SetMouseTextureToStandard();
        }
    }

    public void SetMouseTextureToClick()
    {
        Cursor.SetCursor(clickedCursor, offSet, cursorMode);
    }

    public void SetMouseTextureToStandard()
    {
        Cursor.SetCursor(standardCursor, offSet, cursorMode);
    }
}
