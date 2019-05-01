using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieStateController : StateController
{
    // Add Zombie Settings
    ZombieSettings settings = null;
    Zombie owner = null;

    public Zombie Owner { get => owner; }
    public ZombieSettings Settings { get => settings; }

    

    private void Awake()
    {
        owner = GetComponent<Zombie>();
        settings = owner.Settings;
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }
}
