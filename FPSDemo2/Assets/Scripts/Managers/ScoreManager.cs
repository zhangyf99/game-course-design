using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class ScoreManager : MonoBehaviour
{
    public static int score;
    public int saveScore;

    Text text;
    private BinaryFormatter bf = new BinaryFormatter();   // 二进制格式化程序
    private FileStream fileStream;


    void Awake ()
    {
        text = GetComponent <Text> ();
        if (PlayerPrefs.GetInt("Load", 0) == 1 && File.Exists(Application.dataPath + "/StreamingAssets/bin" +
                PlayerPrefs.GetInt("Level", 0) + ".txt"))
        {
            fileStream = File.Open(Application.dataPath + "/StreamingAssets/bin" +
                PlayerPrefs.GetInt("Level", 0) + ".txt", FileMode.Open);
            Save readSave = (Save)bf.Deserialize(fileStream);
            fileStream.Close();
            score = readSave.score;
            saveScore = score;
        }
        else
        {
            score = 0;
            saveScore = 0;
        }
        
    }


    void Update ()
    {
        text.text = "分数: " + score;
        saveScore = score;
    }
}
