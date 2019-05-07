using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpellUI : MonoBehaviour
{
    [SerializeField] string spellDescription;
    [SerializeField] float maxCoolDown = 1.0f;

    Image coolDownImage;
    TextMeshProUGUI coolDownText;
    float coolDownRemaining;

    public string SpellDescription { get => spellDescription; set => spellDescription = value; }
    public float MaxCoolDown { get => maxCoolDown; set => maxCoolDown = value; }

    private void Awake()
    {
        coolDownRemaining = maxCoolDown;
        coolDownImage = GetComponentInChildren<Image>();
        coolDownText = GetComponentInChildren<TextMeshProUGUI>();
    }

    // FOR TESTING ONLY
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Put this on cooldown
        }
    }

    public void PutSpellOnCoolDown()
    {
        coolDownText.text = coolDownRemaining.ToString();
        coolDownImage.fillAmount = 1.0f;

        // Start Cooldown
        StopCoroutine(CoolDown());
        StartCoroutine(CoolDown());
    }

    IEnumerator CoolDown()
    {
        // While the spell has cooldown left, decrease it
        while (coolDownRemaining > 0)
        {
            coolDownRemaining -= Time.deltaTime;

            // Make cooldown visible
            coolDownImage.fillAmount = coolDownRemaining / maxCoolDown;

            // TODO Make text 0.0 type float
            coolDownText.text = coolDownRemaining.ToString();

            yield return null;
        }

        // Reset the cooldown
        coolDownRemaining = maxCoolDown;
        coolDownText.text = "";

    }

}
