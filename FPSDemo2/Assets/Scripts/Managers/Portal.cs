using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Portal : MonoBehaviour
{
    public float rotateSpeed = 50f;
    //public int portalDamage = 20;
    //public float destroyTime = 15f;
    public GameObject protector = null;
    public bool dead = false;

    public EnemyHealth protectorHealth = null;

    private PortalManager portalManager;
    /*
    private bool isPortal = false;
    private PlayerHealth playerHealth;
    private int index = 0;

    private void Refresh()
    {
        if (isPortal)
        {
            portalManager.hasPortal = false;
        }

        portalManager.hasGenerated[index] = false;
        portalManager.exist--;
        Destroy(gameObject);
    }
    */

    // Start is called before the first frame update
    void Start()
    {
        portalManager = GameObject.FindGameObjectWithTag("portalController").GetComponent<PortalManager>();
        if (protector != null)
        {
            protectorHealth = protector.GetComponent<EnemyHealth>();
        }
        
        /*
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>(); 
        index = portalManager.spawnPointIndex;
        if (!portalManager.hasPortal)
        {
            if(portalManager.exist == 2)
            {
                isPortal = true;
                portalManager.hasPortal = true;
            }
            else
            {
                float p = Random.Range(0, 1);
                if (p < 0.5)
                {
                    isPortal = true;
                    portalManager.hasPortal = true;
                }
            }
        }
        Invoke("Refresh", destroyTime);
        */
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * rotateSpeed);
        if(!dead && protectorHealth.currentHealth <= 0)
        {
            dead = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(dead && other.tag == "Player")
        {
            /*
            if(isPortal)
            {
                SceneManager.LoadScene("next");
            }
            else
            {
                if (playerHealth.currentHealth > 0)
                {
                    playerHealth.TakeDamage(portalDamage);
                }
            }
            portalManager.hasGenerated[index] = false;
            portalManager.exist--;
            */
 
            portalManager.keyGet++;
            portalManager.showKeyHint();

            Destroy(gameObject);
        }
    }
}
