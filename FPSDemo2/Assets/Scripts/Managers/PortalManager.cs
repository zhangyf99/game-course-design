using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortalManager : MonoBehaviour
{
    public GameObject portal;
    public GameObject protector;
    public GameObject boss;
    //public float spawnTime = 3f;
    public Transform[] spawnPoints;
    public Transform[] ProtectorSpawnPoints;
    public Transform bossSpawnPoint;
    public Text keyText;
    public int keyGet = 0;

    //public int exist = 0;
    //public bool hasPortal = false;
    //public bool[] hasGenerated;
    //public int spawnPointIndex = 0;

    //private int limit = 4;
    private GameObject prot;
    private GameObject key;
    private ProtectorMove protM;
    private bool hasBoss = false;

    /*
    void Spawn()
    {
        if (exist >= limit)
        {
            return;
        }

        do
        {
            spawnPointIndex = Random.Range(0, spawnPoints.Length);
        } while (hasGenerated[spawnPointIndex]);

        hasGenerated[spawnPointIndex] = true;
        Instantiate(portal, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        exist++;
    }
    */

    // Start is called before the first frame update
    void Start()
    {
        /*
        hasGenerated = new bool[spawnPoints.Length];
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            hasGenerated[i] = false;
        }
        InvokeRepeating("Spawn", spawnTime, spawnTime);
        */
        keyText.text = "KEY " + keyGet + "/6";

        for (int i = 0; i < spawnPoints.Length && i < ProtectorSpawnPoints.Length; i++)
        {
            prot = (GameObject)Instantiate(protector, ProtectorSpawnPoints[i].position, ProtectorSpawnPoints[i].rotation);
            protM = prot.GetComponent<ProtectorMove>();
            protM.key = spawnPoints[i];
            protM.pos = ProtectorSpawnPoints[i];
            key = (GameObject)Instantiate(portal, spawnPoints[i].position, spawnPoints[i].rotation);
            key.GetComponent<Portal>().protectorHealth = prot.GetComponent<EnemyHealth>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        keyText.text = "KEY " + keyGet + "/6";
        if(!hasBoss && keyGet == 6)
        {
            hasBoss = true;
            Instantiate(boss, bossSpawnPoint.position, bossSpawnPoint.rotation);
        }
    }
}
