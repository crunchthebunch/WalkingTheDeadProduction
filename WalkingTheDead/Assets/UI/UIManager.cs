using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{

    [SerializeField] GameObject spellDescription;
    [SerializeField] Texture2D cursorTexture;
    CursorMode cursorMode = CursorMode.Auto;

    private void Awake()
    {
        Cursor.SetCursor(cursorTexture, Vector2.zero, cursorMode);
    }

    public void ShowSpellDescription(SpellUI spell)
    {
        spellDescription.SetActive(true);

        // Update the spell Text
        spellDescription.GetComponentInChildren<TextMeshProUGUI>().text = spell.SpellDescription;
    }

    public void HideSpellDescription()
    {
        spellDescription.SetActive(false);
    }

    public void ShowStatValue(StatUI stat)
    {
        // Make the value text visible
        stat.gameObject.SetActive(true);
    }

    public void HideStatValue(StatUI stat)
    {
        // Make value invisible
        stat.gameObject.SetActive(false);
    }
}
