using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public float rotateSpeed = 50f;
    public int portalDamage = 20;
    public float destroyTime = 15f;

    private PortalManager portalManager;
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

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        portalManager = GameObject.FindGameObjectWithTag("portalController").GetComponent<PortalManager>();
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
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * rotateSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
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
            Destroy(gameObject);
        }
    }
}
