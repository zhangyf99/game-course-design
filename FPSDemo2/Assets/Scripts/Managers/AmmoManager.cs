using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class AmmoManager : MonoBehaviour
{
    //the time to count
    public int currentAmmo = 30;
    public int currentMaxAmmo = 120;
    public Text AmmoText;
    public Text MaxAmmoText;
    public Gun gun;
    //public bool timeFlying = true;


    // Start is called before the first frame update
    void Start()
    {
        //AmmoText = GameObject.Find("CurrentAmmoText").GetComponent<Text>();
        //MaxAmmoText = GameObject.Find("CurrentMaxAmmoText").GetComponent<Text>();
        //gun = GameObject.Find("arms_assault_rifle_01").GetComponent<AssualtRifle>();
    }

    void Update()
    {
        currentAmmo = gun.GetCurrentAmmo();
        currentMaxAmmo = gun.GetCurrentMaxAmmo();
        AmmoText.text= "弹夹内子弹数:" + currentAmmo;
        MaxAmmoText.text = "未装配子弹数:" + currentMaxAmmo;
    }

}
