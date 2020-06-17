using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public GameObject enemy;
    public float spawnTime = 20f;
    public Transform[] spawnPoints;


    void Start ()
    {
        //para:methodName:string,time:float,repeatTime:float
        //do methodName during time repeat every repeatTime
        InvokeRepeating("Spawn", 0, spawnTime);
    }


    void Spawn ()
    {
        if(playerHealth.currentHealth <= 0f)
        {
            return;
        }

        //random enemy
        int spawnPointIndex = Random.Range (0, spawnPoints.Length);

        Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }
}
