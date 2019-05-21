using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadBodyResurrect : MonoBehaviour
{
    GameManager gameManager;

    Animator anim;

    GameObject playerObject;

    public ParticleSystem soulParticle;
    public ParticleSystem resurrectParticle;
    public ParticleSystem bigboiParticle;

    [SerializeField] GameObject zombieSpawn = null;
    [SerializeField] GameObject bigboiZombieSpawn = null;

    bool getInput = false;

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
        if (Input.GetKeyDown("3") && getInput && gameManager.manaValue >= 40.0f)
        {
            Debug.Log("OHLAWDHECOMIN");
            bigboiParticle.Play();
            Invoke("BigBoiSpawned", 2.0f);
            gameManager.bigBoiManaCostActive = true;

            anim.SetBool("isBigBoiCasting", true);
            Invoke("setbigAnimationFalse", 2.0f);
        }
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

        if (other.gameObject.name == "PlayerCharacter")
        {
            getInput = true;
            Debug.Log("Player entered!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "PlayerCharacter")
        {
            getInput = false;
        }
    }

    private void setAnimationFalse()
    {
        anim.SetBool("isResurrecting", false);
    }

    private void setbigAnimationFalse()
    {
        anim.SetBool("isBigBoiCasting", false);
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

    private void BigBoiSpawned()
    {
        Instantiate(bigboiZombieSpawn, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
}
