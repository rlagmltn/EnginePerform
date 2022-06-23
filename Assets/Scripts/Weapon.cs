using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum Type { melee, range };
    public Type type;
    int damage = 10;
    public float rate;
    public BoxCollider col;
    TrailRenderer TR;
    bool isattack = false;

    [SerializeField]
    private GameObject bullet;
    private void Awake()
    {
        col = GetComponent<BoxCollider>();
        TR = GetComponentInChildren<TrailRenderer>();
    }
    public bool Attack()
    {
        if (isattack) return false;
        if (type == Type.melee)
        {
            StartCoroutine(Swing());
        }
        else
        {
            StartCoroutine(Fire());
        }
        return true;
    }
    IEnumerator Swing()
    {
        isattack = true;
        yield return new WaitForSeconds(0.1f);
        col.enabled = true;
        yield return new WaitForSeconds(0.5f);
        col.enabled = false;
        isattack = false;
        yield return new WaitForSeconds(0.4f);
    }
    IEnumerator Fire()
    {
        isattack = false;
        GameObject obj = Instantiate(bullet, Vector3.forward, Quaternion.identity);
        yield return new WaitForSeconds(0.05f);
        isattack = true;
    }
}
