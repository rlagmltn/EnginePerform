using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSet : MonoBehaviour
{
    [SerializeField]
    private GameObject[] Weapon;

    private void Update()
    {
        if (Input.GetButtonDown("Alpha 1"))
        {
            ChangeWeapon(0);
        }
        if (Input.GetButtonDown("Alpha 2"))
        {
            ChangeWeapon(1);
        }
        if (Input.GetButtonDown("Alpha 3"))
        {
            ChangeWeapon(2);
        }
        if (Input.GetButtonDown("Alpha 4"))
        {
            ChangeWeapon(3);
        }
        //if (Input.GetButtonDown("Alpha 5"))
        //{
        //    ChangeWeapon(4);
        //}
    }
    void ChangeWeapon(int _input)
    {
        for (int i = 0; i < Weapon.Length; i++)
        {
            if (Weapon == null) continue;
            if (i == _input) Weapon[i].SetActive(true);
            else Weapon[i].SetActive(false);
        }
    }
}
