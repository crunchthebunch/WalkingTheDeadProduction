using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    Animator anim;

    public float walkSpeed = 10.0f;

    Quaternion tempQuaternion = Quaternion.Euler(30.0f, 0.0f, 30.0f);



    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();

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



    }




}
