﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public GameObject treasure;

    EnemyHealth health;

    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if(health.isDead)
        {
            Instantiate(treasure, gameObject.transform.position, gameObject.transform.rotation);
            GetComponent<Boss>().enabled = true;
        }
    }
}
