using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Treasure : MonoBehaviour
{
    GameObject player;
    AudioSource audioSource;
    CanvasGroup panelCanvasGroup;

    private bool vic = true;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        panelCanvasGroup = GameObject.FindGameObjectWithTag("Fader").GetComponent<CanvasGroup>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            vic = false;
            Time.timeScale = 0;
            // 删档
            if (File.Exists(Application.dataPath + "/StreamingAssets/bin" + PlayerPrefs.GetInt("Level", 0) + ".txt"))
            {
                File.Delete(Application.dataPath + "/StreamingAssets/bin" + PlayerPrefs.GetInt("Level", 0) + ".txt");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!vic)
        {
            if(panelCanvasGroup.alpha < 1.0f)
            {
                panelCanvasGroup.alpha += 0.01f;
            }
            else
            {
                Time.timeScale = 1;
                SceneManager.LoadScene("Complete");
                GetComponent<Treasure>().enabled = false;
            }
        }
    }
}
