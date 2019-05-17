using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public float playerHealth;

    public float manaValue;
    public float maxMana = 100.0f;

    public float maxHealth = 100.0f;
    public int numberOFZombies;
    bool isPlayerTarget;

    public StatUI healthUI, manaUI;

    bool particleEffectActive;

    LayerMask groundLayerMask;
    ParticleSystem click;
    LoadSceneOnClick sceneLoader;
    PlayerMovement necromancer;

    public bool disguiseManaCostActive;

    public ParticleSystem clickSystemEffect;
    Camera mainCamera;

    // Start is called before the first frame update
    void Awake()
    {
        playerHealth = maxHealth;
        manaValue = 0.0f;
        isPlayerTarget = false;

        click = Instantiate(clickSystemEffect, Vector3.zero, Quaternion.Euler(90.0f, 0.0f, 0.0f));

        groundLayerMask = LayerMask.GetMask("Ground");
        sceneLoader = FindObjectOfType<LoadSceneOnClick>();
        necromancer = FindObjectOfType<PlayerMovement>();

        particleEffectActive = false;
        mainCamera = GameObject.Find("PlayerCharacter/Camera").GetComponent<Camera>();

    }

    private void Start()
    {
        healthUI.SetupStatUI(30.0f, playerHealth, maxHealth);
        manaUI.SetupStatUI(30.0f, manaValue, maxMana);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            PlayParticleEffect();
        }

        disguiseSpellActive();

        healthUI.SetCurrentValue(playerHealth);
        manaUI.SetTargetValue(manaValue);
    }


    public void DecreaseHealth()
    {
        // Decrease Health
        playerHealth -= 10.0f;

        // If Dead Load Lose Screen
        if (playerHealth < 0.0f)
        {
            sceneLoader.LoadLoseScreen();
        }
    }

    void PlayParticleEffect()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, 100.0f, groundLayerMask))
        {
                click.transform.position = hitInfo.point;
                click.Play();
        }
    }

    float CalculateHealth()
    {
        return playerHealth / maxHealth;
    }

    float CalculateMana()
    {
        return manaValue / maxMana;
    }

    void disguiseSpellActive()
    {
        if (disguiseManaCostActive)
        {
            manaValue -= 0.01f;
        }
    }
}