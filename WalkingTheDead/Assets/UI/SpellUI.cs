using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellUI : MonoBehaviour
{

    [SerializeField] string spellDescription;

    public string SpellDescription { get => spellDescription; set => spellDescription = value; }

}
