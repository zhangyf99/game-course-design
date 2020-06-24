using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class next : MonoBehaviour
{
    public Text nextLevel;
    public Text exit;
    //public GameObject InfoUI;

    private bool pos = true;

    // Start is called before the first frame update
    void Start()
    {
        exit.GetComponent<Shadow>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            if(pos)
            {
                SceneManager.LoadScene("Level 02");
                //GameObject.FindGameObjectWithTag("InfoUI").SetActive(true);
                //InfoUI.SetActive(true);
                //GameObject root = GameObject.Find("HUDCanvas");
                //root.transform.Find("InfoUI").gameObject.SetActive(true);
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

        if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            pos = !pos;
            if(pos)
            {
                exit.GetComponent<Shadow>().enabled = false;
                nextLevel.GetComponent<Shadow>().enabled = true;
            }
            else
            {
                exit.GetComponent<Shadow>().enabled = true;
                nextLevel.GetComponent<Shadow>().enabled = false;
            }
        }
    }
}
