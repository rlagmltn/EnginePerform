using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum Type { melee, range };
    public Type type;
    int damage;
    float rate;
    public BoxCollider col;
    TrailRenderer TR;
    bool isattack = false;
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
            //StartCoroutine(Fire());
        }
        return true;
    }
    IEnumerator Swing()
    {
        isattack = true;
        yield return new WaitForSeconds(0.1f);
        col.enabled = true;
        TR.enabled = true;
        yield return new WaitForSeconds(0.5f);
        col.enabled = false;
        isattack = false;
        yield return new WaitForSeconds(0.4f);
        TR.enabled = false;
    }
    //IEnumerator Fire()
    //{
        
    //}
}
