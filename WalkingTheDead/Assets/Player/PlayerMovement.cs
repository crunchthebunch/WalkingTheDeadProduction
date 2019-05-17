﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    Animator anim;

    public float walkSpeed = 10.0f;

    Quaternion tempQuaternion = Quaternion.Euler(30.0f, 0.0f, 30.0f);

    public SphereCollider soulCollectionScanner;
    public SphereCollider resurrectionScanner;
    public SphereCollider fearScanner;
    public float radiusIncrease;
    public ParticleSystem smoke;
    public ParticleSystem pentagram;
    public ParticleSystem resurrectParticle;
    public ParticleSystem soulRingParticle;

    public SpellUI FearUI;
    public SpellUI DisguiseUI;
    public SpellUI BigBoiUI;

    public float FearCooldown = 5.0f;
    public float DisguiseCooldown = 3.0f;
    public float BigBoiCooldown = 3.0f;

    bool soulCollectionActive;
    bool resurrectionActive;

    bool disguiseSpellActive;
    bool fearSpellActive;
    bool zombieSpellActive;
    bool mendFleshSpellActive;
    bool pentagramPlaying;

    private Vector2 horizontalInput;

    public Camera cam;

    private float angle = 0.0f;
    private Quaternion targetRotation = Quaternion.identity;

    public float turnSpeed = 4.0f;

    GameManager gameManager;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        soulCollectionActive = false;
        resurrectionActive = false;
        disguiseSpellActive = false;
        fearSpellActive = false;
        zombieSpellActive = false;
        mendFleshSpellActive = false;


        FearUI.MaxCoolDown = FearCooldown;
        DisguiseUI.MaxCoolDown = DisguiseCooldown;
        BigBoiUI.MaxCoolDown = BigBoiCooldown;

        // Particle System Initializers
        pentagram.Stop();
        resurrectParticle.Stop();
        soulRingParticle.Stop();
        smoke.Stop();
    }

    // Update is called once per frame
    void Update()
    {

        // Animation booleans for Animator
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }

        // Soul Collection Sphere
        if (Input.GetKey("e") && !resurrectionActive && soulCollectionScanner.radius <= 10.0f)
        {
            soulCollectionScanner.radius += radiusIncrease;
            soulRingParticle.Play();
            soulRingParticle.transform.localScale = new Vector3(soulCollectionScanner.radius / 6, 1.0f, soulCollectionScanner.radius / 6);
            soulCollectionActive = true;
            anim.SetBool("isWalking", false);
            walkSpeed = 0.0f;
        }
        else if (!Input.GetKey("e") && soulCollectionScanner.radius > 0.0f)
        {
            soulCollectionScanner.radius -= (radiusIncrease * 2);
            soulRingParticle.transform.localScale = new Vector3(soulCollectionScanner.radius / 6, 1.0f, soulCollectionScanner.radius / 6);
            walkSpeed = 3.0f;
        }
        else if (soulCollectionScanner.radius <= 0.0f)
        {
            soulCollectionActive = false;
            soulRingParticle.Stop();
        }

        // Resurrection Sphere
        if (Input.GetKey("r") && !soulCollectionActive && resurrectionScanner.radius <= 10.0f)
        {
            resurrectionScanner.radius += radiusIncrease;
            resurrectParticle.Play();
            resurrectParticle.transform.localScale = new Vector3(resurrectionScanner.radius/6, 1.0f, resurrectionScanner.radius/6);
            resurrectionActive = true;
            anim.SetBool("isWalking", false);
            anim.SetBool("isResurrecting", true);
            walkSpeed = 0.0f;
        }
        else if (!Input.GetKey("r") && resurrectionScanner.radius > 0.0f)
        {
            resurrectionScanner.radius -= (radiusIncrease * 2);
            resurrectParticle.transform.localScale = new Vector3(resurrectionScanner.radius/6, 1.0f, resurrectionScanner.radius/6);
            anim.SetBool("isResurrecting", false);
            walkSpeed = 3.0f;
        }
        else if (resurrectionScanner.radius <= 0.0f)
        {
            resurrectionActive = false;
            resurrectParticle.Stop();
        }

        // Disguise Spell
        if (Input.GetKeyUp("2") && disguiseSpellActive == false && gameManager.manaValue > 0.0f && !DisguiseUI.IsOnCoolDown)
        {
            this.tag = "Disguise";
            gameManager.disguiseManaCostActive = true;
            DisguiseUI.HoverSpell();
            Debug.Log("DISGUISE");
            smoke.Play();
            disguiseSpellActive = true;

        }

        else if ((Input.GetKeyUp("2") && disguiseSpellActive == true) || (gameManager.manaValue <= 0.0f && disguiseSpellActive == true))
        {
            disguiseSpellActive = false;
            this.tag = "Necromancer";
            DisguiseUI.PutSpellOnCoolDown();
            DisguiseUI.StopHoveringSpell();
            gameManager.disguiseManaCostActive = false;
            smoke.Stop();
        }

        // Fear Spell
        if (Input.GetKeyUp("1") && gameManager.manaValue > 20.0f && !FearUI.IsOnCoolDown && !fearSpellActive)
        {
            fearSpellActive = true;
            FearUI.PutSpellOnCoolDown();
            FearUI.HoverSpell();
            pentagram.Play();
            Debug.Log("FEAR");
        }
        else
        {
            //pentagram.Stop();
            FearUI.StopHoveringSpell();
            fearSpellActive = false;
        }

        GetInput();

        if (Mathf.Abs(horizontalInput.x) < 1 && Mathf.Abs(horizontalInput.y) < 1)
        {
            return;
        }

        CalculateDirection();
        Rotate();
        Move();

    }

    void GetInput()
    {
        horizontalInput.x = Input.GetAxis("Horizontal");
        horizontalInput.y = Input.GetAxis("Vertical");
    }

    void CalculateDirection()
    {
        angle = Mathf.Atan2(horizontalInput.x, horizontalInput.y);
        angle = Mathf.Rad2Deg * angle;
        angle += cam.transform.eulerAngles.y;
    }

    void Rotate()
    {
        targetRotation = Quaternion.Euler(0, angle, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed);
    }

    void Move()
    {
        transform.position += transform.forward * walkSpeed * Time.deltaTime;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, soulCollectionScanner.radius);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, resurrectionScanner.radius);
    }
}
