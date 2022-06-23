using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSet : MonoBehaviour
{
    [SerializeField]
    protected GameObject[] Weapon;
    protected Animator ani;
    public Material playerMaterial;
    bool playerChangable = true;

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
            bool attackable = equipWeapon.Attack();
            if (attackable)
                StartCoroutine(AttackAnim(equipWeapon.type));
        }
    }
    IEnumerator AttackAnim(Weapon.Type _type)
    {
        if (_type == global::Weapon.Type.melee)
        {
            ani.SetTrigger("Swing");
            yield return new WaitForSeconds(equipWeapon.rate);
        }
        else
        {
            ani.SetTrigger("Fire");
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
