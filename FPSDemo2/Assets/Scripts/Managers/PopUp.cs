using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PopUp : MonoBehaviour
{
    public Text hint;
    public GameObject icon;
    public Text newGame;
    public Text cont;
    public Text re;

    private BinaryFormatter bf = new BinaryFormatter();   // 二进制格式化程序
    private FileStream fileStream;

    menu control;
    AudioSource changeClickAudio;
    private int pos = 0;
    private int choice = 0;


    void Awake()
    {
        control = GameObject.FindGameObjectWithTag("MenuController").GetComponent<menu>();
        changeClickAudio = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        hint.text = "";
        pos = 0;
        choice = PlayerPrefs.GetInt("Level", 0);
        control.enabled = false;
        cont.GetComponent<Shadow>().enabled = false;
        re.GetComponent<Shadow>().enabled = false;
        icon.transform.position = new Vector2(icon.transform.position.x, newGame.transform.position.y);
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
            if(pos == 0)
            {
                PlayerPrefs.SetInt("Load", pos);
                //SceneManager.LoadScene("Level 0" + (choice + 1).ToString());
                SceneManager.LoadScene("Mail");
            }
            else if(pos == 1)
            {
                // 读档
                if (File.Exists(Application.dataPath + "/StreamingAssets/bin" + choice + ".txt"))
                {
                    //SceneManager.LoadScene("Level 0" + (choice + 1).ToString());
                    PlayerPrefs.SetInt("Load", pos);
                    SceneManager.LoadScene("Loading");
                }
                else
                {
                    hint.text = "没有存档记录或已通关";
                    Invoke("endHint", 1.0f);
                }
            }
            else  // 返回
            {
                control.enabled = true;
                //GetComponent<PopUp>().enabled = false;
                gameObject.SetActive(false);
            }
        }

        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            changeClickAudio.Play();
            pos = (pos == 0) ? 2 : pos - 1;
            show();
        }
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            changeClickAudio.Play();
            pos = (pos == 2) ? 0 : pos + 1;
            show();
        }
    }

    private void endHint()
    {
        hint.text = "";
    }

    private void show()
    {
        if(pos == 0)
        {
            newGame.GetComponent<Shadow>().enabled = true;
            cont.GetComponent<Shadow>().enabled = false;
            re.GetComponent<Shadow>().enabled = false;
            icon.transform.position = new Vector2(icon.transform.position.x, newGame.transform.position.y);
        }
        else if(pos == 1)
        {
            newGame.GetComponent<Shadow>().enabled = false;
            cont.GetComponent<Shadow>().enabled = true;
            re.GetComponent<Shadow>().enabled = false;
            icon.transform.position = new Vector2(icon.transform.position.x, cont.transform.position.y);
        }
        else
        {
            newGame.GetComponent<Shadow>().enabled = false;
            cont.GetComponent<Shadow>().enabled = false;
            re.GetComponent<Shadow>().enabled = true;
            icon.transform.position = new Vector2(icon.transform.position.x, re.transform.position.y);
        }
    }
}
