using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    //the time to count
    public int countDown = 120;
    //public bool timeFlying = true;

    private BinaryFormatter bf = new BinaryFormatter();   // 二进制格式化程序
    private FileStream fileStream;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("Load", 0) == 1 && File.Exists(Application.dataPath + "/StreamingAssets/bin" +
                PlayerPrefs.GetInt("Level", 0) + ".txt"))
        {
            fileStream = File.Open(Application.dataPath + "/StreamingAssets/bin" +
                PlayerPrefs.GetInt("Level", 0) + ".txt", FileMode.Open);
            Save readSave = (Save)bf.Deserialize(fileStream);
            fileStream.Close();
            countDown = readSave.time;
        }
        GetComponent<UnityEngine.UI.Text>().text="时间:"+ countDown + "秒";
        InvokeRepeating("TimeCount", 0.0f, 1.0f);
    }

    void TimeCount()
	{
        if(countDown > 0)
		{
            //countDown = (timeFlying) ? countDown - 1 : countDown;
            countDown--;
            GetComponent<UnityEngine.UI.Text>().text = "时间:" + countDown + "秒";
        }
        else
		{
            CancelInvoke();
		}
	}

}
