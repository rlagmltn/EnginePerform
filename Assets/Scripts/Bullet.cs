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
        Die(3);
    }
    void Update()
    {
        Move();
    }
    void Move()
    {
        transform.Translate(Vector3.forward * moveSpeed);
    }
    void Die(int _t = 0)
    {
        Destroy(gameObject , _t);
    }
}
