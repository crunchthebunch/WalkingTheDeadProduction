using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadBodyResurrect : MonoBehaviour
{
    GameManager gameManager;

    Animator anim;

    GameObject playerObject = null;

    [SerializeField] GameObject zombieSpawn = null;

    // Start is called before the first frame update
    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        playerObject = GameObject.Find("PlayerCharacter");
        anim = playerObject.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "ResurrectScanner")
        {
            Invoke("InstantiateZombie", 0.5f);
        }

        else if (other.gameObject.name == "SoulCollectionScanner")
        {
            Invoke("SoulCollected", 0.5f);
        }
    }

    private void setAnimationFalse()
    {
        anim.SetBool("isResurrecting", false);
    }

    private void InstantiateZombie()
    {
        Instantiate(zombieSpawn, transform.position, transform.rotation);
        gameManager.numberOFZombies += 1;
        Destroy(this.gameObject);
    }

    private void SoulCollected()
    {
        Destroy(this.gameObject);
    }
}
