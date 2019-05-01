using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerStateController : StateController
{
    VillagerSettings settings = null;
    Villager owner = null;

    public Villager Owner { get => owner; }
    public VillagerSettings Settings { get => settings; }

    private void Awake()
    {
        owner = GetComponent<Villager>();
        settings = owner.Settings;
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }
}
