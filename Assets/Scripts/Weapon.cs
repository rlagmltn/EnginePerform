using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum Type { melee, range, granade };
    public Type type;
    public int damage;
    public float rate;
    public int curammo;
    public int maxAmmo;

    public Collider col;
    TrailRenderer TR;
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
        col = GetComponent<Collider>();
        TR = GetComponentInChildren<TrailRenderer>();
        if (type == Type.granade) TR.enabled = false;
    }
    public void Attack()
    {
        if (type == Type.melee)
        {
            StartCoroutine(Swing());
        }
        else if (type == Type.range && curammo > 0)
        {
            curammo--;
            Fire();
        }
        else if (type == Type.granade && curammo > 0)
        {
            Granade();
        }
    }
    IEnumerator Swing()
    {
        col.enabled = true;
        yield return new WaitForSeconds(0.5f);
        col.enabled = false;
    }
    void Fire()
    {
        GameObject bObj = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
        Vector3 bDir = new Vector3(bulletPos.forward.x, 0f, bulletPos.forward.z);
        Rigidbody bRigid = bObj.GetComponent<Rigidbody>();
        bRigid.velocity = bDir * 50f;


        GameObject cObj = Instantiate(bulletCase, bulletCasePos.position, bulletCasePos.rotation);
        Rigidbody cRigid = cObj.GetComponent<Rigidbody>();
        Vector3 cDir = bulletCasePos.forward * Random.Range(-3, -2) - Vector3.up * Random.Range(2, 3);
        cRigid.AddForce(cDir, ForceMode.Impulse);
        cRigid.AddTorque(10 * Vector3.up, ForceMode.Impulse);
    }
    void Granade()
    {
        GameObject bObj = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
    }
}
