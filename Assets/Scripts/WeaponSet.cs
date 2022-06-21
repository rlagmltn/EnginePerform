using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSet : MonoBehaviour
{
    [SerializeField]
    protected GameObject[] Weapon;
    protected Animator ani;
    public Material playerMaterial;
    bool playerChangable = false;

    public enum PlayerState
    {
        IRON,
        FIRE,
        THUNDER,
        WATER
    }
    public PlayerState PS = PlayerState.IRON;
    Weapon equipWeapon;
    private void Awake()
    {
        ani = GetComponentInChildren<Animator>();
        equipWeapon = GetComponentInChildren<Weapon>();
    }
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
        if (Input.GetButtonDown("Swap1"))
        {
            int swap = (int)Input.GetAxisRaw("Swap2");
            StartCoroutine(ChangePlayerState(swap));
        }
        if (Input.GetMouseButton(0))
        {
            bool attackable =  equipWeapon.Attack();
            if (attackable) ani.SetTrigger("Swing");
        }
    }
    void ChangeWeapon(int _input)
    {
        ani.SetTrigger("Swap");
        for (int i = 0; i < Weapon.Length; i++)
        {
            if (Weapon == null) continue;
            if (i == _input)
            {
                Weapon[i].SetActive(true);
                equipWeapon = Weapon[i].GetComponent<Weapon>();
            }
            else Weapon[i].SetActive(false);
        }
    }
    IEnumerator ChangePlayerState(int _switch)
    {
        if (!playerChangable) yield break;
        playerChangable = false;
        if (PS <= 0 && _switch == -1) PS = PlayerState.WATER;
        else if (PS >= PlayerState.WATER && _switch == 1) PS = 0;
        else PS += _switch;
        Debug.Log($"{_switch} : {PS}");
        switch (PS)
        {
            case PlayerState.FIRE:
                playerMaterial.color = Color.red;
                break;
            case PlayerState.THUNDER:
                playerMaterial.color = Color.yellow;
                break;
            case PlayerState.WATER:
                playerMaterial.color = Color.blue;
                break;
            case PlayerState.IRON:
            default:
                playerMaterial.color = Color.white;
                break;
        }
        yield return new WaitForSeconds(5f);
        playerChangable = true;
    }
}
