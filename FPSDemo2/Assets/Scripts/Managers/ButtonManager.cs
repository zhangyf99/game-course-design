using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public GameObject Minimap;
    //public GameObject Hint;
    public Button MapButton;
    public Text MapText;
    public bool hintShow;
    

    /* 存档 */
    PortalManager portalManager;
    AmmoManager ammoManager;
    GameObject player;
    public GameObject keyParent;
    //public GameObject protectorParent;
    public GameObject monsterParent;
    public GameObject timeManager;
    public GameObject scoreManager;
    private BinaryFormatter bf = new BinaryFormatter();   // 二进制格式化程序
    private FileStream fileStream;
    /*     */

    // Start is called before the first frame update
    void Start()
    {
        portalManager = GameObject.FindGameObjectWithTag("portalController").GetComponent<PortalManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        timeManager.transform.GetComponent<TimeManager>();
        ammoManager = GetComponent<AmmoManager>();

        Minimap = GameObject.FindWithTag("MiniMap");
        //Hint = GameObject.FindWithTag("Hint");
        //hintShow = Hint.activeSelf;
        MapButton = GameObject.Find("MapButton").GetComponent<Button>();
        MapText = GameObject.Find("MapText").GetComponent<Text>();
        //MapButton.onClick.AddListener(MapOnClick);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            MapOnClick();
            //Debug.Log(MapText.text);
        }
        /*
        //Debug.Log(count);
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            HintOnClick();
            //Debug.Log(MapText.text);
        }
        */
        if (Input.GetKeyDown(KeyCode.Q))
        {
            BackMenu();
        }
    }

    public void ShowMap()
    {
        Minimap.SetActive(true);
        //MapText.text = "HideMap";
    }

    public void HideMap()
    {
        Minimap.SetActive(false);
        //MapText.text = "ShowMap";
    }

    /*
    public void ShowHint()
    {
        Hint.SetActive(true);
    }

    public void HideHint()
    {
        Hint.SetActive(false);
    }
    */

    public void MapOnClick()
    {
        if (MapText.text == "显 示 地 图")
        {
            ShowMap();
            MapText.text = "隐 藏 地 图";
        }
        else
        {
            HideMap();
            MapText.text = "显 示 地 图";
        }
    }

    /*
    public void HintOnClick()
    {
        if (hintShow)
        {
            HideHint();
        }
        else
        {
            ShowHint();
        }
        hintShow = !hintShow;
    }
    */

    public void BackMenu()
    {
        if(portalManager.hasBoss)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            Time.timeScale = 0;
            SaveFile();
            Time.timeScale = 1;
            SceneManager.LoadScene(0);
        }
    }

    private void SaveFile()
    {
        Save save = new Save();
        /* 玩家 */
        save.playerHealth = player.GetComponent<PlayerHealth>().currentHealth;
        save.playerPosition.Add(player.transform.position.x);
        save.playerPosition.Add(player.transform.position.y);
        save.playerPosition.Add(player.transform.position.z);
        /* 小怪 */
        foreach (Transform child in monsterParent.transform)
        {
            List<float> temp = new List<float>();
            temp.Add(child.position.x);
            temp.Add(child.position.y);
            temp.Add(child.position.z);
            save.monster.Add(temp);
            save.monsterHealth.Add(child.gameObject.GetComponent<EnemyHealth>().currentHealth);
            save.monsterNum++;
        }
        /* 钥匙和钥匙守护者 */
        foreach (Transform child in keyParent.transform)
        {
            List<float> temp = new List<float>();
            temp.Add(child.position.x);
            temp.Add(child.position.y);
            temp.Add(child.position.z);
            save.key.Add(temp);
            save.keyNum++;
            if (child.gameObject.GetComponent<Portal>().dead)
            {
                save.hasProtector.Add(false);
            }
            else
            {
                save.hasProtector.Add(true);
                GameObject p = child.gameObject.GetComponent<Portal>().protector;
                save.protectorHealth.Add(p.GetComponent<EnemyHealth>().currentHealth);
                save.protectorType.Add(p.GetComponent<ProtectorMove>().type);
                save.protectorNum++;
            }
        }
        /* 时间和分数 */
        save.time = timeManager.GetComponent<TimeManager>().countDown;
        save.score = scoreManager.GetComponent<ScoreManager>().saveScore;
        /* 子弹数 */
        save.ammoNum = ammoManager.currentAmmo;
        save.maxAmmoNum = ammoManager.currentMaxAmmo;

        fileStream = File.Create(Application.dataPath + "/StreamingAssets/bin" +
                PlayerPrefs.GetInt("Level", 0) + ".txt");
        bf.Serialize(fileStream, save);
        fileStream.Close();
    }

}
