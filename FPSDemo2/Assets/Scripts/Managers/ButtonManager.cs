using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public GameObject Minimap;
    public Button MapButton;
    public Text MapText;

    // Start is called before the first frame update
    void Start()
    {
        Minimap = GameObject.FindWithTag("MiniMap");
        MapButton = GameObject.Find("MapButton").GetComponent<Button>();
        MapText = GameObject.Find("MapText").GetComponent<Text>();
        MapButton.onClick.AddListener(MapOnClick);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            MapOnClick();
            //Debug.Log(MapText.text);
        }
        //Debug.Log(count);
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

    public void MapOnClick()
    {
        if(MapText.text == "ShowMap")
        {
            ShowMap();
            MapText.text = "HideMap";
        }
        else
        {
            HideMap();
            MapText.text = "ShowMap";
        }
    }
}
