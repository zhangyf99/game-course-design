using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public GameObject enemy;
    public float spawnTime = 20f;
    public Transform[] spawnPoints;

    private BinaryFormatter bf = new BinaryFormatter();   // 二进制格式化程序
    private FileStream fileStream;
    private float startTime = 0;

    void Start ()
    {
        if (PlayerPrefs.GetInt("Load", 0) == 1 && File.Exists(Application.dataPath + "/StreamingAssets/bin" +
                PlayerPrefs.GetInt("Level", 0) + ".txt"))
        {
            startTime = spawnTime;
            fileStream = File.Open(Application.dataPath + "/StreamingAssets/bin" +
                PlayerPrefs.GetInt("Level", 0) + ".txt", FileMode.Open);
            Save readSave = (Save)bf.Deserialize(fileStream);
            fileStream.Close();

            for(int i = 0; i < readSave.monsterNum; i++)
            {
                List<float> po = readSave.monster[i];
                Vector3 temp = new Vector3(po[0], po[1], po[2]);
                GameObject i_enemy = Instantiate(enemy, temp, spawnPoints[0].rotation) as GameObject;
                i_enemy.transform.parent = gameObject.transform;
                i_enemy.GetComponent<EnemyHealth>().currentHealth = readSave.monsterHealth[i];
            }
        }


        //para:methodName:string,time:float,repeatTime:float
        //do methodName during time repeat every repeatTime
        InvokeRepeating("Spawn", startTime, spawnTime);
    }


    void Spawn ()
    {
        if(playerHealth.currentHealth <= 0f)
        {
            return;
        }

        //random enemy
        int spawnPointIndex = Random.Range (0, spawnPoints.Length);

        GameObject i_enemy = Instantiate(enemy, spawnPoints[spawnPointIndex].position, 
            spawnPoints[spawnPointIndex].rotation) as GameObject;
        i_enemy.transform.parent = gameObject.transform;
    }
}
