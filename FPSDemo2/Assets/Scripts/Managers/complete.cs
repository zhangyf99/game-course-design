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
        hint.text = "Return to the start menu in " + last + " seconds...";
    }

    private void counter()
    {
        last--;
        if (last < 0)
        {
            SceneManager.LoadScene("menu");
        }
        else
        {
            hint.text = "Return to the start menu in " + last + " seconds...";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
