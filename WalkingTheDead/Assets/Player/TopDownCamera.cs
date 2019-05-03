﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TopDownCamera : MonoBehaviour
{
    public Transform target;
    public float height = 30.0f;
    public float distance = 10.0f;
    public float angle = 45.0f;
    public float smoothing = 0.0f;
    public float scrollspeed = 15.0f;
    public float minScroll = 20.0f;
    public float maxScroll = 30.0f;
    public float rotationSpeed = 2.0f;

    private Vector3 offset;

    private Vector3 refVelocity;

    // Start is called before the first frame update
    void Start()
    {
        HandleCamera();
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


        if (Input.GetMouseButton(2))
        {
            offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotationSpeed, Vector3.up) * offset;
            transform.position = offset;
            transform.LookAt(target.position);
        }
        else
        {
            // World Position Vector
            Vector3 worldPosition = (Vector3.forward * -distance) + (Vector3.up * height);

            // Movement of camera
            Vector3 flatTargetPosition = target.position;
            flatTargetPosition.y = 0.0f;
            Vector3 finalposition = flatTargetPosition + worldPosition;

            transform.position = finalposition;

            transform.LookAt(target.position);

            float scroll = Input.GetAxis("Mouse ScrollWheel");

            height = Mathf.Clamp(height, minScroll, maxScroll);

            height -= scroll * scrollspeed * 20.0f * Time.deltaTime;

            offset = finalposition;
        }
    }
}
