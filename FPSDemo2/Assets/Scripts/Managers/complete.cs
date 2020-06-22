using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class complete : MonoBehaviour
{
    public Text hint;

    private float last = 3f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("counter", 1.0f, 1.0f);
        hint.text = last + " 秒后为您返回主菜单页...";
    }

    private void counter()
    {
        last--;
        hint.text = last + " 秒后为您返回主菜单页...";
        if(last <= 0)
        {
            SceneManager.LoadScene("menu");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
