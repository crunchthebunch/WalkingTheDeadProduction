using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSoldierStateController : StateController
{

    MeleeSoldier owner;
    MeleeSoldierSettings settings;

    public MeleeSoldier Owner { get => owner; }
    public MeleeSoldierSettings Settings { get => settings; }

    // Test Print
    public void TestPrint(float distance)
    {
        print(distance);
    }

    private void Awake()
    {
        owner = GetComponent<MeleeSoldier>();
        settings = owner.Settings;
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }
}
