using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float moveSpeed = 10f;

    TrailRenderer TR;
    private void Awake()
    {
        TR = GetComponent<TrailRenderer>();
    }
    private void Start()
    {
        Destroy(gameObject ,4f);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {

        }
        if (collision.gameObject.CompareTag("Object"))
        {
            Destroy(collision.gameObject);
        }
    }
}
