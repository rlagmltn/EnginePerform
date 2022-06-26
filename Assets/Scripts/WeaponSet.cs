using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSet : MonoBehaviour
{
    [SerializeField]
    protected GameObject[] WeaponObject;
    protected Animator ani;
    public Material playerMaterial;
    bool playerChangable = true;
    public enum PlayerState
    {
        IRON,
        WATER,
        THUNDER,
        FIRE
    }
    public PlayerState PS = PlayerState.IRON;
    Weapon equipWeapon;
    private void Awake()
    {
        ani = GetComponentInChildren<Animator>();
        equipWeapon = GetComponentInChildren<Weapon>();
        StartCoroutine(ChangePlayerState(0));
    }
    private void Update()
    {
        if (Input.GetButtonDown("Alpha 1"))
        {
            StartCoroutine(ChangePlayerState(0));
        }
        if (Input.GetButtonDown("Alpha 2"))
        {
            StartCoroutine(ChangePlayerState(1));
        }
        if (Input.GetButtonDown("Alpha 3"))
        {
            StartCoroutine(ChangePlayerState(2));
        }
        if (Input.GetButtonDown("Alpha 4"))
        {
            StartCoroutine(ChangePlayerState(3));
        }
        if (Input.GetMouseButton(0))
        {
            Attack();
        }
    }
    void Attack()
    {

        StartCoroutine(AttackAnim());
    }
    IEnumerator AttackAnim()
    {
        ani.SetTrigger(equipWeapon.type == Weapon.Type.melee ? "Swing" : "Fire");
        yield return new WaitForSeconds(equipWeapon.rate);
    }
    void ChangeWeapon(int _input)
    {
        ani.SetTrigger("Swap");
        for (int i = 0; i < WeaponObject.Length; i++)
        {
            if (WeaponObject == null) continue;
            if (i == _input)
            {
                WeaponObject[i].SetActive(true);
                equipWeapon = WeaponObject[i].GetComponent<Weapon>();
            }
            else WeaponObject[i].SetActive(false);
        }
    }
    IEnumerator ChangePlayerState(int _switch)
    {
        if (!playerChangable) yield break;
        playerChangable = false;
        PS = (PlayerState)_switch;
        Debug.Log($"{_switch} : {PS}");
        ChangeWeapon(_switch);
        ChangeColor();
        yield return new WaitForSeconds(5f);
        playerChangable = true;
    }
    void ChangeColor()
    {
        playerMaterial.color = PS switch
        {
            PlayerState.FIRE => Color.red,
            PlayerState.THUNDER => Color.yellow,
            PlayerState.WATER => Color.blue,
            PlayerState.IRON => Color.white,
            _ => Color.white,
        };
    }
}
