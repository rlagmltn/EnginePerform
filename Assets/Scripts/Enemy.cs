using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private int hp = 40;
    Animator ani;
    Rigidbody rigid;
    Collider col;
    Material mat;
    NavMeshAgent NMA;
    public Transform target;
    private void Awake()
    {
        hp = 400;
        ani = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        mat = GetComponentInChildren<Renderer>().material;
        NMA = GetComponent<NavMeshAgent>();
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
            Debug.Log($"{hp} , {value}");
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
    void Update()
    {
        transform.LookAt(target.position);
        rigid.velocity = (target.position - transform.position).normalized * 5f;
        Debug.Log(rigid.velocity);
    }
    void FixedUpdate()
    {
        Freeze();
    }
    void Freeze()
    {
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;
    }
    public IEnumerator Hit()
    {
        Color tmp = mat.color;
        mat.color = Color.red;
        yield return new WaitForSeconds(0.05f);
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
        if (col.gameObject.CompareTag("Bullet"))
        {
            Bullet bullet = col.GetComponent<Bullet>();
            HP = bullet.damage;
            Destroy(col.gameObject);
        }
        if (col.gameObject.CompareTag("Melee"))
        {
            Weapon weapon = col.GetComponent<Weapon>();
            HP = weapon.damage;
        }
    }
}
