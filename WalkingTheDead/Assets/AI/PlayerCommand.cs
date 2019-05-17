using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCommand : MonoBehaviour
{
    Camera mainCamera;

    LayerMask groundLayer;

    Vector3 lastPositionGiven = Vector3.zero;

    //For Command Event
    public delegate void ClickAction(Vector3 position);
    public static event ClickAction Click;

    private void Awake()
    {
        groundLayer = LayerMask.GetMask("Ground");
        mainCamera = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Click != null && Input.GetMouseButtonDown(0))
        {
            Click(GetCommandPosition());
        }
        else if (Click != null && Input.GetMouseButtonDown(1))
        {
            Click(transform.position);
        }
    }

    Vector3 GetCommandPosition()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, 1000f, groundLayer))
        {
            lastPositionGiven = hitInfo.point;
            return hitInfo.point;
        }
        else return Vector3.negativeInfinity;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(lastPositionGiven, 0.5f);
    }
}
