using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class SpellUI : MonoBehaviour
{
    [SerializeField] string spellDescription;
    [SerializeField] double maxCoolDown = 1.0f;

    Image coolDownImage;
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
        coolDownImage = FindChildImage();
    }

    private Image FindChildImage()
    {
        // Find all images
        var imageComponents = GetComponentsInChildren<Image>();
        Image foundImage = imageComponents[0];

        // Find the first object that is from the child component and NOT the parent
        foreach (Image image in imageComponents)
        {
            if (image.gameObject.name != gameObject.name)
                foundImage = image;
        }

        return foundImage;
    }

    private void Start()
    {
        coolDownImage.fillAmount = 0.0f;
        coolDownText.text = "";
    }

    // FOR TESTING ONLY
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Put this on cooldown
            PutSpellOnCoolDown();
        }
    }

    public void PutSpellOnCoolDown()
    {
        if (!isOnCoolDown)
        {
            coolDownText.text = coolDownRemaining.ToString();
            coolDownImage.fillAmount = 1.0f;
            isOnCoolDown = true;

            // Start Cooldown
            StopCoroutine(CoolDown());
            StartCoroutine(CoolDown());
        }
        else
        {
            // Light up Border
        }
        
    }

    IEnumerator CoolDown()
    {
        // While the spell has cooldown left, decrease it
        while (coolDownRemaining > 0.0f)
        {
            coolDownRemaining -= Time.deltaTime;

            // Make cooldown visible
            coolDownImage.fillAmount = (float)(coolDownRemaining / maxCoolDown);

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
