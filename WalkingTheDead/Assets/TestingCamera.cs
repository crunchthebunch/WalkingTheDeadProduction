using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingCamera : MonoBehaviour
{
    float rotationInput;
    float offsetInput;
    Transform player;

    public float speed = 20.0f;
    public Vector3 offset;



    void Awake()
    {
        player = GameObject.Find("PlayerCharacter").GetComponent<Transform>();
    }

    private void Update()
    {
        GetInput();
        Rotate();
        MoveOffset();
        MoveWithPlayerY();
    }

    void FixedUpdate()
    {

    }

    void GetInput()
    {
        rotationInput = Input.GetAxisRaw("Camera Horizontal");
        offsetInput = Input.GetAxisRaw("Camera Vertical");
    }

    void Rotate()
    {
        transform.RotateAround(player.transform.position, Vector3.up, -rotationInput * speed * Time.deltaTime);
        if (rotationInput != 0) offset = transform.position - player.transform.position;
    }

    void MoveOffset()
    {
        offset = Vector3.MoveTowards(offset, player.transform.position, offsetInput * speed/2 * Time.deltaTime);
    }

    void MoveWithPlayerY()
    {
        Vector3 desiredPosition = player.transform.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, speed / 7.5f * Time.deltaTime);
        transform.LookAt(player.transform.position);
    }

    void FollowPlayer()
    {
        transform.position = Vector3.Lerp(transform.position, player.transform.position + offset, speed / 10 * Time.deltaTime);
        transform.LookAt(player.transform);
    }



}