using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    //the input num
    public int count = 0;
    private int weaponNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        weaponNum = transform.childCount;
        //choose the first by default
        SelectWeapon(0);
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            count = (count + 1)% weaponNum;
            SelectWeapon(count);
        }
        //Debug.Log(count);
    }

    void SelectWeapon(int index)
    {
        //chhose the weapon
        for (var i = 0; i < weaponNum; i++)
        {
            if (i == index)
            {
                transform.GetChild(i).gameObject.SetActive(true);

            }
            else transform.GetChild(i).gameObject.SetActive(false);

        }
    }
 }
