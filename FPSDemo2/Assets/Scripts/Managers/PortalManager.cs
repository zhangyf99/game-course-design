using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class PortalManager : MonoBehaviour
{
    public GameObject portal;
    public GameObject protector;
    public GameObject boss;
    //public float spawnTime = 3f;
    public Transform[] KeySpawnPoints;
    public Transform[] ProtectorSpawnPoints;
    public Transform bossSpawnPoint;
    public Text keyText;
    public int keyGet = 0;
    public bool hasBoss = false;
    public GameObject keyParent;
    public GameObject protectorParent;

    //public int exist = 0;
    //public bool hasPortal = false;
    //public bool[] hasGenerated;
    //public int spawnPointIndex = 0;

    //private int limit = 4;
    private GameObject prot;
    private GameObject key;
    private ProtectorMove protM;

    private BinaryFormatter bf = new BinaryFormatter();   // 二进制格式化程序
    private FileStream fileStream;

    /*
    void Spawn()
    {
        if (exist >= limit)
        {
            return;
        }

        do
        {
            spawnPointIndex = Random.Range(0, KeyKeySpawnPoints.Length);
        } while (hasGenerated[spawnPointIndex]);

        hasGenerated[spawnPointIndex] = true;
        Instantiate(portal, KeyKeySpawnPoints[spawnPointIndex].position, KeyKeySpawnPoints[spawnPointIndex].rotation);
        exist++;
    }
    */

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt("Load", 0) == 1 && File.Exists(Application.dataPath + "/StreamingAssets/bin" +
                PlayerPrefs.GetInt("Level", 0) + ".txt"))
        {
            fileStream = File.Open(Application.dataPath + "/StreamingAssets/bin" +
                PlayerPrefs.GetInt("Level", 0) + ".txt", FileMode.Open);
            Save readSave = (Save)bf.Deserialize(fileStream);
            fileStream.Close();
            keyGet = 6 - readSave.keyNum;
            for(int i = 0, j = 0; i < readSave.keyNum; i++)
            {
                List<float> po = readSave.key[i];
                Vector3 temp = new Vector3(po[0], po[1], po[2]);
;               key = (GameObject)Instantiate(portal, temp, KeySpawnPoints[0].rotation);
                key.transform.parent = keyParent.transform;
                if(readSave.hasProtector[i])
                {
                    int typ = readSave.protectorType[j];
                    prot = (GameObject)Instantiate(protector, ProtectorSpawnPoints[typ].position, 
                        ProtectorSpawnPoints[typ].rotation);
                    prot.GetComponent<EnemyHealth>().currentHealth = readSave.protectorHealth[j];
                    prot.transform.parent = protectorParent.transform;
                    protM = prot.GetComponent<ProtectorMove>();
                    protM.key = key.transform;
                    protM.pos = ProtectorSpawnPoints[typ];
                    protM.type = typ;
                    key.GetComponent<Portal>().protector = prot;
                    j++;
                }
                else
                {
                    key.GetComponent<Portal>().dead = true;
                }
            }
        }
        else
        {
            for (int i = 0; i < KeySpawnPoints.Length && i < ProtectorSpawnPoints.Length; i++)
            {
                prot = (GameObject)Instantiate(protector, ProtectorSpawnPoints[i].position, ProtectorSpawnPoints[i].rotation);
                prot.transform.parent = protectorParent.transform;
                protM = prot.GetComponent<ProtectorMove>();
                protM.key = KeySpawnPoints[i];
                protM.pos = ProtectorSpawnPoints[i];
                protM.type = i;
                key = (GameObject)Instantiate(portal, KeySpawnPoints[i].position, KeySpawnPoints[i].rotation);
                key.transform.parent = keyParent.transform;
                key.GetComponent<Portal>().protector = prot;
            }
        }

        keyText.text = "钥匙 " + keyGet + "/6";
    }

    // Update is called once per frame
    void Update()
    {
        keyText.text = "钥匙 " + keyGet + "/6";
        if(!hasBoss && keyGet == 6)
        {
            hasBoss = true;
            Instantiate(boss, bossSpawnPoint.position, bossSpawnPoint.rotation);
        }
    }
}
