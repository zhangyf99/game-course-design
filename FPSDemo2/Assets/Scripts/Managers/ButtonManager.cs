using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public GameObject Minimap;
    public GameObject Hint;
    public Button MapButton;
    public Text MapText;
    public bool hintShow;

    // Start is called before the first frame update
    void Start()
    {
        Minimap = GameObject.FindWithTag("MiniMap");
        //Hint = GameObject.FindWithTag("Hint");
        hintShow = Hint.activeSelf;
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
        //Debug.Log(count);
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            HintOnClick();
            //Debug.Log(MapText.text);
        }

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

    public void ShowHint()
    {
        Hint.SetActive(true);
    }

    public void HideHint()
    {
        Hint.SetActive(false);
    }

    public void MapOnClick()
    {
        if(MapText.text == "显 示 地 图")
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

    public void BackMenu()
    {
        SceneManager.LoadScene(0);
    }

}
