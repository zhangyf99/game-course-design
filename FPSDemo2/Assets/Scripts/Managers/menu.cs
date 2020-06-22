using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menu : MonoBehaviour
{
    public Text message;
    public Text start;
    public Text exit;
    public Text continued;

    private BinaryFormatter bf = new BinaryFormatter();   // 二进制格式化程序
    private FileStream fileStream;

    private int pos = 0;

    private void Awake()
    {
        message.text = "";
        Cursor.visible = false;
        exit.GetComponent<Shadow>().enabled = false;
        continued.GetComponent<Shadow>().enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void endMessage()
    {
        message.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            if(pos == 0)
            {
                Save save = new Save();
                fileStream = File.Create(Application.dataPath + "/StreamingAssets/bin.txt");
                bf.Serialize(fileStream, save);
                fileStream.Close();
                SceneManager.LoadScene("main1");
                SceneManager.LoadScene("Level 01");
            }
            else if(pos == 1)
            {
                if (File.Exists(Application.dataPath + "/StreamingAssets/bin.txt"))
                {
                    fileStream = File.Open(Application.dataPath + "/StreamingAssets/bin.txt", FileMode.Open);
                    Save readSave = (Save)bf.Deserialize(fileStream);
                    fileStream.Close();
                    if (readSave.level >= 2)
                    {
                        message.text = "您已通过全部关卡";
                        Invoke("endMessage", 1.0f);
                    }
                    else
                    {
                        SceneManager.LoadScene("Level 0" + (readSave.level + 1).ToString());
                    }
                }
                else
                {
                    message.text = "没有存档记录";
                    Invoke("endMessage", 1.0f);
                }
            }
            else
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                     Application.Quit();
#endif
            }
        }
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            pos = (pos == 2) ? 0 : ++pos;
            show();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            pos = (pos == 0) ? 2 : --pos;
            show();
        }
    }

    private void show()
    {
        if (pos == 0)
        {
            exit.GetComponent<Shadow>().enabled = false;
            continued.GetComponent<Shadow>().enabled = false;
            start.GetComponent<Shadow>().enabled = true;
        }
        else if (pos == 1)
        {
            continued.GetComponent<Shadow>().enabled = true;
            start.GetComponent<Shadow>().enabled = false;
            exit.GetComponent<Shadow>().enabled = false;
        }
        else
        {
            exit.GetComponent<Shadow>().enabled = true;
            start.GetComponent<Shadow>().enabled = false;
            continued.GetComponent<Shadow>().enabled = false;
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
