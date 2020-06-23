using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    EnemyHealth health;

    private BinaryFormatter bf = new BinaryFormatter();   // 二进制格式化程序
    private FileStream fileStream;

    private int level = 0;
    private bool haswrite = false;

    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if(health.isDead && !haswrite)
        {
            haswrite = true;
            GameObject.FindGameObjectWithTag("TimeController").GetComponent<TimeManager>().timeFlying = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().cannotHurt = true;
            if (File.Exists(Application.dataPath + "/StreamingAssets/bin.txt"))
            {
                FileStream fileStream = File.Open(Application.dataPath + "/StreamingAssets/bin.txt", FileMode.Open);
                Save save = (Save)bf.Deserialize(fileStream);
                fileStream.Close();
                save.level++;
                fileStream = File.Create(Application.dataPath + "/StreamingAssets/bin.txt");
                bf.Serialize(fileStream, save);
                fileStream.Close();
                level = save.level;
                Invoke("load", 1.5f);
            }
        }
    }

    private void load()
    {
        if (level == 1)
        {
            SceneManager.LoadScene("Level 02");
        }
        else if(level == 2)
        {
            SceneManager.LoadScene("complete");
        }
    }
}
