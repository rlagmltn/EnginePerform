using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    private void Start()
    {
        damage = FindObjectOfType<WeaponSet>().equipWeapon.damage;
        Destroy(gameObject , 4f);
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Object"))
        {
            Destroy(collision.gameObject);
        }
    }
}
