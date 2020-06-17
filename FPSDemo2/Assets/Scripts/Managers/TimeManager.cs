using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    //the time to count
    int countDown = 30;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<UnityEngine.UI.Text>().text="Time:"+ countDown + "s";
        InvokeRepeating("TimeCount", 0.0f, 1.0f);
    }

    void TimeCount()
	{
        if(countDown > 0)
		{
            countDown--;
            GetComponent<UnityEngine.UI.Text>().text = "Time:" + countDown + "s";
        }
        else
		{
            CancelInvoke();
		}
	}

}
