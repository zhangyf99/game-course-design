using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalManager : MonoBehaviour
{
    public GameObject portal;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;
    public int exist = 0;
    public bool hasPortal = false;
    public bool[] hasGenerated;
    public int spawnPointIndex = 0;

    private int limit = 4;

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

    // Start is called before the first frame update
    void Start()
    {
        hasGenerated = new bool[spawnPoints.Length];
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            hasGenerated[i] = false;
        }
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
