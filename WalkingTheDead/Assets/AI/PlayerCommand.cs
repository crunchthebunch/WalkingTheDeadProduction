using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCommand : MonoBehaviour
{
    Camera mainCamera;
    Scanner zombieScanner;

    LayerMask groundLayer;

    //For Command Event
    public delegate void ClickAction(Vector3 position, bool followPlayer);
    public static event ClickAction Click;

    void Start()
    {
        zombieScanner.SetupScanner("Zombie", 20f);
    }

    private void Awake()
    {
        zombieScanner = GetComponentInChildren<Scanner>();
        groundLayer = LayerMask.GetMask("Ground");
        mainCamera = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Click != null && Input.GetMouseButtonDown(0))
        {
            Click(GetCommandPosition(), false);
        }
        else if (Click != null && Input.GetMouseButtonDown(1))
        {
            Click(transform.position, true);
        }
    }

    Vector3 GetCommandPosition()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, 1000f, groundLayer))
        {
            return hitInfo.point;
        }
        else return Vector3.negativeInfinity;
    }
}
