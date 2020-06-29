using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Save
{
    public int level = 0;

    public int playerHealth = 0;
    public List<float> playerPosition = new List<float>();

    public List<List<float>> monster = new List<List<float>> ();
    public List<int> monsterHealth = new List<int>();
    public int monsterNum = 0;

    public List<List<float>> key = new List<List<float>>();
    public int keyNum = 0;
    public List<bool> hasProtector = new List<bool>();

    public List<int> protectorHealth = new List<int>();
    public List<int> protectorType = new List<int>();
    public int protectorNum = 0;

    public int time = 0;
    public int score = 0;
    public int ammoNum = 0;
    public int maxAmmoNum = 0;
}
