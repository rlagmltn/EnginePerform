using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int hp = 40;
    Animator ani;
    Rigidbody rigid;
    Collider col;
    Material mat;
    private void Awake()
    {
        hp = 400;
        ani = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        mat = GetComponent<Renderer>().material;
    }
    public int HP
    {
        get
        {
            return hp;
        }
        set
        {
            hp -= value;
            if (hp <= 0)
            {
                Die();
            }
            else
            {
                StartCoroutine(Hit());
            }
        }
    }
    public IEnumerator Hit(int _atk = 1)
    {
        Color tmp = mat.color;
        mat.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        mat.color = tmp;
    }
    void Die()
    {
        Destroy(gameObject, 1f);
        mat.color = Color.black;
        ani.SetTrigger("Die");
    }
    private void OnTriggerEnter(Collider col)
    {
        Vector3 rVec = (transform.position - col.transform.position).normalized;
        if (col.gameObject.CompareTag("Bullet"))
        {
            Bullet bullet = col.GetComponent<Bullet>();
            HP -= bullet.damage;
            rVec = (transform.position - col.transform.position).normalized;
        }
        if (col.gameObject.CompareTag("Melee"))
        {
            Weapon weapon = col.GetComponent<Weapon>();
            HP -= weapon.damage;
        }
        rVec += Vector3.up;
        rigid.AddForce(rVec * 5, ForceMode.Impulse);
        Debug.Log(HP);
    }
}
