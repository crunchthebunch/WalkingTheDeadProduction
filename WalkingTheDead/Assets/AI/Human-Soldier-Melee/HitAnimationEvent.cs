using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitAnimationEvent : MonoBehaviour
{
    AttackMeleeSoldierBehaviour attackBehaviour;
    GameManager gameManager;
    Scanner enemyScanner;
    GameObject closestEnemy = null;

    public void SetClosestEnemy(GameObject closestEnemy)
    {
        this.closestEnemy = closestEnemy;
    }

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        attackBehaviour = GetComponentInParent<AttackMeleeSoldierBehaviour>();
    }

    public void KillEnemy()
    {

        if (closestEnemy)
        {
            Zombie zombie = closestEnemy.GetComponent<Zombie>();

            // See if this is a zombie
            if (zombie)
            {
                zombie.Die(); // TODO Add delayed animation trigger event
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
            attackBehaviour.AttackCoolDown();

        }
    }
}
