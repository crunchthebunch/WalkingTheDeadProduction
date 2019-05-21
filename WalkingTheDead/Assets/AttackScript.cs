using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    Scanner parentsScanner;
    GameManager manager;

    private void Awake()
    {
        parentsScanner = transform.parent.GetComponentInChildren<Scanner>();
        manager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AttackComplete()
    {
        GameObject toKill = parentsScanner.GetClosestTargetInRange();
        if (toKill)
        {
            KillEnemy(toKill);
        }
    }
    void KillEnemy(GameObject toKill)
    {
        Villager villager = toKill.GetComponent<Villager>();
        if (villager)
        {
            villager.Die();
            return;
        }

        Zombie zombie = toKill.GetComponent<Zombie>();
        if (zombie)
        {
            zombie.Die();
            return;
        }

        MeleeSoldier soldier = toKill.GetComponent<MeleeSoldier>();
        if (soldier)
        {
            soldier.Die();
            return;
        }

        if (toKill.CompareTag("Necromancer"))
        {
            manager.DecreaseHealth();
        }
        return;
    }
}
