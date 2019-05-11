using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    Animator anim;

    public float walkSpeed = 10.0f;

    Quaternion tempQuaternion = Quaternion.Euler(30.0f, 0.0f, 30.0f);

    public SphereCollider soulCollectionScanner;
    public SphereCollider resurrectionScanner;
    public float radiusIncrease;

    bool soulCollectionActive;
    bool resurrectionActive;

    bool disguiseSpellActive;

    private Vector2 horizontalInput;

    public Camera cam;

    private float angle = 0.0f;
    private Quaternion targetRotation = Quaternion.identity;

    public float turnSpeed = 4.0f;

    public GameManager gameManager;

    public GameObject meshRendererObject;
    SkinnedMeshRenderer skinnedMeshRenderer;
    public Material disguiseMaterial;
    public Material defaultMaterial;



    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();

        soulCollectionActive = false;
        resurrectionActive = false;
        disguiseSpellActive = false;

        skinnedMeshRenderer = meshRendererObject.GetComponent<SkinnedMeshRenderer>();
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
            soulCollectionActive = true;
            anim.SetBool("isWalking", false);
            walkSpeed = 0.0f;
        }
        else if (!Input.GetKey("e") && soulCollectionScanner.radius > 0.0f)
        {
            soulCollectionScanner.radius -= (radiusIncrease * 3);
            walkSpeed = 3.0f;
        }
        else if (soulCollectionScanner.radius <= 0.0f)
        {
            soulCollectionActive = false;
        }

        // Resurrection Sphere
        if (Input.GetKey("r") && !soulCollectionActive && resurrectionScanner.radius <= 10.0f)
        {
            resurrectionScanner.radius += radiusIncrease;
            resurrectionActive = true;
            anim.SetBool("isWalking", false);
            walkSpeed = 0.0f;
        }
        else if (!Input.GetKey("r") && resurrectionScanner.radius > 0.0f)
        {
            resurrectionScanner.radius -= (radiusIncrease * 3);
            walkSpeed = 3.0f;
        }
        else if (resurrectionScanner.radius <= 0.0f)
        {
            resurrectionActive = false;
        }

        // Disguise Spell
        if (Input.GetKeyUp("1") && disguiseSpellActive == false && gameManager.manaValue > 0.0f)
        {
            this.tag = "Disguise";
            gameManager.disguiseManaCostActive = true;
            skinnedMeshRenderer.material = disguiseMaterial;
            disguiseSpellActive = true;
            
        }
        else if (Input.GetKeyUp("1") && disguiseSpellActive == true || gameManager.manaValue <= 0.0f)
        {
            this.tag = "Necromancer";
            gameManager.disguiseManaCostActive = false;
            skinnedMeshRenderer.material = defaultMaterial;
            disguiseSpellActive = false;
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
        horizontalInput.x = Input.GetAxisRaw("Horizontal");
        horizontalInput.y = Input.GetAxisRaw("Vertical");
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
