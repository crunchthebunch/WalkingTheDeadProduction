﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Behaviour : MonoBehaviour
{
    public abstract void DoBehaviour();

    public abstract void SetupBehaviour(AISettings settings);
}
