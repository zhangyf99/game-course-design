using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fps_Input : MonoBehaviour
{
    public class fps_InputAxis
    {
        public KeyCode positive;
        public KeyCode negative;
    }

    public Dictionary<string, KeyCode> buttons = new Dictionary<string, KeyCode>();

    public Dictionary<string, fps_InputAxis> axis = new Dictionary<string, fps_InputAxis>();

    public List<string> unityAxis = new List<string>();

    private void setupDefaults(string type = "")
    {
        if(type == "" || type == "buttons")
        {
            if(buttons.Count == 0)
            {
                addButton("Fire", KeyCode.Mouse0);
                //addButton("Reload", KeyCode.R);   // 换弹
                addButton("Jump", KeyCode.Space);
                addButton("Crouch", KeyCode.C);   // 蹲伏
                addButton("Sprint", KeyCode.LeftShift);   // 冲刺
                addButton("Map", KeyCode.Keypad1);   // 地图
            }
        }

        if (type == "" || type == "Axis")
        {
            if (axis.Count == 0)
            {
                addAxis("Horizontal", KeyCode.W, KeyCode.S);
                addAxis("Vertical", KeyCode.A, KeyCode.D);
            }
        }

        if (type == "" || type == "unityAxis")
        {
            if (unityAxis.Count == 0)
            {
                addUnityAxis("Mouse X");
                addUnityAxis("Mouse Y");
                addUnityAxis("Horizontal");
                addUnityAxis("Vertical");
            }
        }
    }

    private void addButton(string n, KeyCode k)
    {
        if(buttons.ContainsKey(n))
        {
            buttons[n] = k;
        }
        else
        {
            buttons.Add(n, k);
        }
    }

    private void addAxis(string n, KeyCode pk, KeyCode nk)
    {
        if(axis.ContainsKey(n))
        {
            axis[n] = new fps_InputAxis() { positive = pk, negative = nk };
        }
        else
        {
            axis.Add(n, new fps_InputAxis() { positive = pk, negative = nk });
        }
    }

    private void addUnityAxis(string n)
    {
        if(!unityAxis.Contains(n))
        {
            unityAxis.Add(n);
        }
    }

    public bool getButton(string button)
    {
        if (buttons.ContainsKey(button))
        {
            return Input.GetKey(buttons[button]);
        }
        else
        {
            return false;
        }
    }

    public bool getbuttonDown(string button)
    {
        if (buttons.ContainsKey(button))
        {
            return Input.GetKeyDown(buttons[button]);
        }
        else
        {
            return false;
        }
    }

    public float getAxis(string a)
    {
        if (this.unityAxis.Contains(a))
        {
            return Input.GetAxis(a);
        }
        else
            return 0;
    }

    public float getAxisRaw(string a)
    {
        if(this.axis.ContainsKey(a))
        {
            if (Input.GetKey(this.axis[a].positive))
                return 1;
            if (Input.GetKey(this.axis[a].negative))
                return -1;

            return 0;
        }
        else if(unityAxis.Contains(a))
        {
            return Input.GetAxisRaw(a);
        }
        else
        {
            return 0;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        setupDefaults();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
