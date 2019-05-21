using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UISpell : MonoBehaviour
{
    [SerializeField] string spellDescription;
    [SerializeField] double maxCoolDown = 1.0f;
    AudioSource onCoolDownSound = null;

    // Images
    [SerializeField] Image standard = null;
    [SerializeField] Image coolDown = null;
    [SerializeField] Image hovered = null;


    TextMeshProUGUI coolDownText;
    double coolDownRemaining;
    bool isOnCoolDown = false;

    public string SpellDescription { get => spellDescription; set => spellDescription = value; }
    public double MaxCoolDown { get => maxCoolDown; set => maxCoolDown = value; }
    public bool IsOnCoolDown { get => isOnCoolDown; }

    private void Awake()
    {
        coolDownRemaining = maxCoolDown;
        coolDownText = GetComponentInChildren<TextMeshProUGUI>();
        onCoolDownSound = GetComponentInParent<AudioSource>();
    }

    private void Start()
    {
        coolDown.fillAmount = 0.0f;
        coolDownText.text = "";
    }

    public void PutSpellOnCoolDown()
    {
        if (!isOnCoolDown)
        {
            coolDownText.text = coolDownRemaining.ToString();
            coolDown.fillAmount = 1.0f;
            isOnCoolDown = true;

            // Start Cooldown
            StopCoroutine(CoolDown());
            StartCoroutine(CoolDown());
        }
        else
        {
            // Play Sound
            if (!onCoolDownSound.isPlaying)
            {
                onCoolDownSound.Play();
            }
        }
        
    }

    public void HoverSpell()
    {
        hovered.gameObject.SetActive(true);
    }

    public void StopHoveringSpell()
    {
        hovered.gameObject.SetActive(false);
    }

    IEnumerator CoolDown()
    {
        // While the spell has cooldown left, decrease it
        while (coolDownRemaining > 0.0f)
        {
            coolDownRemaining -= Time.deltaTime;

            // Make cooldown visible
            coolDown.fillAmount = (float)(coolDownRemaining / maxCoolDown);

            // Show value with 1 decimal
            coolDownText.text = System.Math.Round(coolDownRemaining, 1).ToString();

            yield return null;
        }

        // Reset the cooldown
        coolDownRemaining = maxCoolDown;
        coolDownText.text = "";
        isOnCoolDown = false;

    }

}
