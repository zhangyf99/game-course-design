using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectorMove : MonoBehaviour
{
    public Transform key;
    public Transform pos;
    public int type;

    Transform player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    UnityEngine.AI.NavMeshAgent nav;

    private float maxDis = 15f;

    void Awake()
    {
        //find player by tag
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        {
            if(Vector3.Distance(gameObject.transform.position, key.position) < maxDis 
                && Vector3.Distance(player.position, key.position) < maxDis)
            {
                // auto compute
                nav.SetDestination(player.position);
            }
            else
            {
                nav.SetDestination(pos.position);
            }
        }
        else
        {
            nav.enabled = false;
        }
    }
}
