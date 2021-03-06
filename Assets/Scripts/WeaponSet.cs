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
    bool attackable = true;
    public bool reloadAble = true;
    public bool isReload = false;
    public int ammo = 120;
    public enum PlayerState
    {
        IRON,
        WATER,
        THUNDER,
        FIRE
    }
    public PlayerState PS = PlayerState.IRON;
    public Weapon equipWeapon;
    private void Awake()
    {
        ani = GetComponentInChildren<Animator>();
        equipWeapon = GetComponentInChildren<Weapon>();
        equipWeapon.curammo = equipWeapon.maxAmmo;
        StartCoroutine(ChangePlayerState(0));
        reloadAble = true;
    }
    private void Update()
    {
        if (!isReload)
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
            if (Input.GetButtonDown("Reload"))
            {
                StartCoroutine(Reload());
            }
        }
        if (Input.GetMouseButton(0))
        {
            StartCoroutine(Attack());
        }
    }
    IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.1f);
        if (equipWeapon.type == Weapon.Type.range && equipWeapon.curammo > 0 && !isReload) ani.SetTrigger("Fire");
        if (attackable && !isReload)
        {
            if (equipWeapon.type == Weapon.Type.melee || equipWeapon.type == Weapon.Type.granade) ani.SetTrigger("Swing");
            attackable = false;
            equipWeapon.Attack();
            yield return new WaitForSeconds(equipWeapon.rate);
            attackable = true;
        }
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
    IEnumerator Reload()
    {
        if (equipWeapon == null || isReload) yield break;
        if (equipWeapon.curammo == equipWeapon.maxAmmo) yield break;
        if (equipWeapon.type == Weapon.Type.melee || equipWeapon.type == Weapon.Type.granade) yield break;
        reloadAble = false; isReload = true;
        ani.SetTrigger("Reload");
        yield return new WaitForSeconds(2f);
        int reAmmo = ammo < equipWeapon.maxAmmo ? ammo : equipWeapon.maxAmmo - equipWeapon.curammo;
        Debug.Log(reAmmo);
        equipWeapon.curammo = reAmmo;
        ammo -= reAmmo;
        reloadAble = true; isReload = false;
    }
}
