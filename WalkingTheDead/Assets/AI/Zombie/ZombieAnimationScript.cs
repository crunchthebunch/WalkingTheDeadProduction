using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAnimationScript : MonoBehaviour
{
    AttackZombieBehaviour attackBehaviour;
    GameObject closestEnemy = null;
    GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        attackBehaviour = GetComponentInParent<AttackZombieBehaviour>();
    }

    public void SetClosestEnemy(GameObject closestEnemy)
    {
        this.closestEnemy = closestEnemy;
    }

    public void KillEnemy()
    {

        if (closestEnemy)
        {
            MeleeSoldier soldier = closestEnemy.GetComponent<MeleeSoldier>();
            Villager villager = closestEnemy.GetComponent<Villager>();

            // See if this is a zombie
            if (soldier)
            {
                soldier.Die(); // TODO Add delayed animation trigger event
            }
            // If it's a villager
            else if(villager)
            {
                villager.Die();
            }
            // Else its a necromancer
            else
            {
                PlayerMovement necroMancer = closestEnemy.GetComponent<PlayerMovement>();

                if (necroMancer)
                {
                    gameManager.DecreaseHealth();
                }
            }
        }

        // Have attack cooldown - if its alive
        if (attackBehaviour)
            attackBehaviour.AttackCoolDown();
    }

}
