﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TopDownCamera : MonoBehaviour
{
    public Transform target;
    public float height;
    public float distance;
    public float angle;
    public float smoothing;
    public float scrollspeed;
    public float minScroll;
    public float maxScroll;
    public float rotationSpeed;

    private Vector3 offset;

    private Vector3 refVelocity;
    private float scroll;


    // Start is called before the first frame update
    void Start()
    {
        offset = new Vector3(target.position.x, target.position.y + height, target.position.z + distance);
        transform.position = target.position + offset;
        transform.LookAt(target.position);

        //HandleCamera();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        HandleCamera();
    }

    protected virtual void HandleCamera()
    {
        if (!target)
        {
            return;
        }

        transform.position = target.position + offset;
        transform.LookAt(target.position);

        scroll = Input.GetAxisRaw("Mouse ScrollWheel");

        height = Mathf.Clamp(height, minScroll, maxScroll);

        height -= scroll * scrollspeed * 20.0f * Time.deltaTime;

        distance = Mathf.Clamp(height, minScroll, maxScroll);

        distance -= scroll * scrollspeed * 20.0f * Time.deltaTime;

        offset.y = target.position.y + height;

        if (Input.GetMouseButton(2))
        {
            offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotationSpeed, Vector3.up) * offset;
        }

    }
}
