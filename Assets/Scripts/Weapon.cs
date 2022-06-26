using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum Type { melee, range, granade };
    public Type type;
    int damage = 2;
    public float rate;
    public BoxCollider col;
    bool isattack = false;

    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private GameObject bulletCase;
    [SerializeField]
    private Transform bulletCasePos;
    [SerializeField]
    private Transform bulletPos;
    private void Awake()
    {
        col = GetComponent<BoxCollider>();
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
        GameObject bObj =  Instantiate(bullet, bulletPos.position, bulletPos.rotation);
        Rigidbody bRigid = bObj.GetComponent<Rigidbody>();
        bRigid.velocity = bulletPos.forward * 50f;

        GameObject cObj = Instantiate(bulletCase, bulletCasePos.position, bulletCasePos.rotation);
        Rigidbody cRigid = cObj.GetComponent<Rigidbody>();
        Vector3 cDir = bulletCasePos.forward * Random.Range(-3, -2) - Vector3.up * Random.Range(2, 3);
        cRigid.AddForce(cDir, ForceMode.Impulse);
        cRigid.AddTorque(10 * Vector3.up, ForceMode.Impulse);
        yield return new WaitForSeconds(rate);
        isattack = true;
    }
}
