using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granade : MonoBehaviour
{
    Rigidbody rigid;
    public int damage = 12;
    public GameObject meshobj;
    public GameObject effectobj;
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        rigid.velocity = transform.forward * 15f;
        StartCoroutine(Explosion());
    }
    IEnumerator Explosion()
    {
        yield return new WaitForSeconds(2f);
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;
        meshobj.SetActive(false);
        effectobj.SetActive(true);
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, 15, Vector3.up, 0f, LayerMask.GetMask("Enemy"));
        foreach (RaycastHit hitobj in hits)
        {
            hitobj.transform.GetComponent<Enemy>().HP = damage;
        }

        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

}
