using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int hp = 3;
    Animator ani;
    private void Awake()
    {
        ani = GetComponentInChildren<Animator>();
    }
    public int HP
    {
        get
        {
            return hp;
        }
        set
        {
            hp += value;
            if (hp <= 0)
            {
                Die();
            }
        }
    }
    public void Hit(int _atk = 1)
    {
        HP -= _atk;
    }
    void Die()
    {
        Destroy(gameObject, 2f);
        ani.SetTrigger("Die");
    }
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Bullet"))
        {
            Hit();
        }
    }
}
