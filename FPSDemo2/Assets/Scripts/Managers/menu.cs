using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menu : MonoBehaviour
{
    public Text level_1;
    public Text level_2;
    public Text exit;
    public GameObject panel;
    //public GameObject InfoUI;

    private bool isPanel = false;
    private int pos = 0;

    AudioSource changeClickAudio;

    void Awake()
    {
        Cursor.visible = false;
        panel.SetActive(false);
        exit.GetComponent<Shadow>().enabled = false;
        level_2.GetComponent<Shadow>().enabled = false;
        GetComponent<menu>().enabled = true;
        changeClickAudio = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            if (pos == 2)
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                     Application.Quit();
#endif
            }
            else
            {
                PlayerPrefs.SetInt("Level", pos);
                isPanel = true;
                panel.SetActive(true);

                //GameObject.Find("InfoUI").SetActive(true);
                //InfoUI.SetActive(true);
                //GameObject.Find("HUDCanvas/InfoUI").SetActive(true);
                //GameObject root = GameObject.Find("HUDCanvas");
                //root.transform.Find("InfoUI").gameObject.SetActive(true);*/
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            changeClickAudio.Play();
            pos = (pos == 0) ? 2 : pos - 1;
            show();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            changeClickAudio.Play();
            pos = (pos == 2) ? 0 : pos + 1;
            show();
        }
    }

    private void show()
    {
        if (pos == 0)
        {
            level_1.GetComponent<Shadow>().enabled = true;
            level_2.GetComponent<Shadow>().enabled = false;
            exit.GetComponent<Shadow>().enabled = false;
        }
        else if (pos == 1)
        {
            level_1.GetComponent<Shadow>().enabled = false;
            level_2.GetComponent<Shadow>().enabled = true;
            exit.GetComponent<Shadow>().enabled = false;
        }
        else
        {
            level_1.GetComponent<Shadow>().enabled = false;
            level_2.GetComponent<Shadow>().enabled = false;
            exit.GetComponent<Shadow>().enabled = true;
        }
    }


    /*
    public void start()
    {
        // 存储数据
        Save save = new Save();
        fileStream = File.Create(Application.dataPath + "/StreamingAssets/bin.txt");
        bf.Serialize(fileStream, save);
        fileStream.Close();
        SceneManager.LoadScene("main1");
    }

    public void continueGame()
    {
        if (File.Exists(Application.dataPath + "/StreamingAssets/bin.txt"))
        {
            fileStream = File.Open(Application.dataPath + "/StreamingAssets/bin.txt", FileMode.Open);
            Save readSave = (Save)bf.Deserialize(fileStream);
            fileStream.Close();
            if (readSave.level >= 3)
            {
                message.text = "您已通过全部关卡";
                Invoke("endMessage", 1.0f);
            }
            else
            {
                SceneManager.LoadScene("main" + (readSave.level + 1).ToString());
            }
        }
        else
        {
            message.text = "没有存档记录";
            Invoke("endMessage", 1.0f);
        }
    }

    public void tutorial()
    {
        //SceneManager.LoadScene("");
    }

    public void exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
                     Application.Quit();
#endif
    }
    */
}
