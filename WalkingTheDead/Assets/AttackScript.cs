using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    Scanner parentsScanner;
    GameManager manager;
    StateController parentsController;
    AISettings settings;

    float damage = 0;
    float maxRandom = 0.2f;

    private void Awake()
    {
        parentsScanner = transform.parent.GetComponentInChildren<Scanner>();
        manager = FindObjectOfType<GameManager>();
        parentsController = GetComponentInParent<StateController>();
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
        parentsController.AttackBehaviour.AttackComplete = true;
    }
    void KillEnemy(GameObject toKill)
    {
        Villager villager = toKill.GetComponent<Villager>();
        if (villager)
        {
            villager.TakeDamage(damage);
            return;
        }

        Zombie zombie = toKill.GetComponent<Zombie>();
        if (zombie)
        {
            zombie.TakeDamage(damage);
            return;
        }

        MeleeSoldier soldier = toKill.GetComponent<MeleeSoldier>();
        if (soldier)
        {
            soldier.TakeDamage(damage);
            return;
        }

        if (toKill.CompareTag("Necromancer"))
        {
            manager.DecreaseHealth();
        }
        return;
    }

    public void SetupAttack(AISettings settings)
    {
        this.settings = settings;
        SetupDamage();
    }

    void SetupDamage()
    {
        damage = settings.AttackDamage;
        damage = Randomize(damage);
    }
    float Randomize(float value)
    {
        return Random.Range(1 - maxRandom, 1 + maxRandom) * value;
    }
}
