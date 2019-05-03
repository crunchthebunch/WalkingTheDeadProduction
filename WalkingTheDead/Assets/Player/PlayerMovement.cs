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

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();

        soulCollectionActive = false;
        resurrectionActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = Vector3.zero;
        movement.x = moveHorizontal;
        movement.z = moveVertical;

        if (movement != Vector3.zero)
        {
            transform.Translate(movement * walkSpeed * Time.deltaTime, Space.World);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15f);
            tempQuaternion = transform.rotation;
        }

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
        }
        else if (!Input.GetKey("e") && soulCollectionScanner.radius > 0.0f)
        {
            soulCollectionScanner.radius -= (radiusIncrease * 3);
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
        }
        else if (!Input.GetKey("r") && resurrectionScanner.radius > 0.0f)
        {
            resurrectionScanner.radius -= (radiusIncrease * 3);
        }
        else if (resurrectionScanner.radius <= 0.0f)
        {
            resurrectionActive = false;
        }



    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, soulCollectionScanner.radius);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, resurrectionScanner.radius);
    }
}
