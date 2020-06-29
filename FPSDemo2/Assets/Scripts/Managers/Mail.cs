using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mail : MonoBehaviour
{
    public AudioClip bgClip;
    public GameObject paper1;
    public GameObject paper2;
    public GameObject hint;

    public bool isMail = true;

    CanvasGroup paperCanvasGroup;
    GameObject paperSelec;
    CanvasGroup hintCanvasGroup;
    AudioSource BgAudio;

    private float add = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt("Level", 0) == 0)
        {
            paperSelec = paper1;
        }
        else
        {
            paperSelec = paper2;
        }
        paperCanvasGroup = paperSelec.GetComponent<CanvasGroup>();
        hintCanvasGroup = hint.GetComponent<CanvasGroup>();
        BgAudio = GetComponent<AudioSource>();
        Invoke("onBGM", 1.0f); 
    }

    private void onBGM()
    {
        BgAudio.clip = bgClip;
        BgAudio.loop = true;
        BgAudio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(paperCanvasGroup.alpha < 1.0f)
        {
            paperCanvasGroup.alpha += 0.01f;
        }
        if(paperSelec.transform.localScale.x < 1.0f)
        {
            /*
            paper.transform.localScale = new Vector3(paper.transform.localScale.x + 0.01f,
                paper.transform.localScale.y + 0.01f, paper.transform.localScale.z);
            */
            paperSelec.transform.localScale += new Vector3(0.01f, 0.01f, 0);
        }
        if (hintCanvasGroup.alpha == 0.0f)
        {
            add = 0.01f;
        }
        if (hintCanvasGroup.alpha == 1.0f)
        {
            add = -0.01f;
        }
        hintCanvasGroup.alpha += add;
        
        if(Input.GetKeyDown(KeyCode.Return))
        {
            if(isMail)
            {
                SceneManager.LoadScene("Loading");
            }
            else
            {
                SceneManager.LoadScene("menu");
            }
        }
    }
}
