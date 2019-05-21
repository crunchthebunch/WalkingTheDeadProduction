using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadBodyResurrect : MonoBehaviour
{
    GameManager gameManager;

    Animator anim;

    GameObject playerObject = null;

    public ParticleSystem soulParticle;
    public ParticleSystem resurrectParticle;
    public ParticleSystem bigboiParticle;

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
            resurrectParticle.Play();
            
            Invoke("InstantiateZombie", 3.5f);
        }

        else if (other.gameObject.name == "SoulCollectionScanner")
        {
            soulParticle.Play();
            
            Invoke("SoulCollected", 2.5f);
        }

        else if (other.gameObject.name == "PlayerCharacter")
        {
            if (Input.GetKeyDown("3") && gameManager.manaValue >= 50.0f)
            {
                anim.SetBool("isBigBoiCasting", true);
                
            }
        }
    }

    private void setAnimationFalse()
    {
        anim.SetBool("isResurrecting", false);
    }

    private void InstantiateZombie()
    {
        Instantiate(zombieSpawn, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }

    private void SoulCollected()
    {
        gameManager.CollectSoul();
        Destroy(this.gameObject);
    }
}
