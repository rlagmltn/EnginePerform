using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody rigid;
    Animator ani;
    PlayerRotate PR;

    bool isDodge = false;
    bool dodgeAble = true;
    private float movespeed = 15f;

    WaitForSeconds waitDodge = new WaitForSeconds(1f);
    WaitForSeconds waitDash = new WaitForSeconds(0.2f);
    Vector3 moveDir = Vector3.zero;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        ani = GetComponentInChildren<Animator>();
        PR = FindObjectOfType<PlayerRotate>();
    }
    private void Update()
    {
        GetInput();
        Move();
        Dodge();
    }
    void GetInput()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        moveDir = new Vector3(x, 0f, z).normalized * movespeed;
        if (dodgeAble) isDodge = Input.GetButtonDown("Dodge");
    }
    void Move()
    {
        if (!isDodge) rigid.velocity = moveDir;
        ani.SetBool("isRun", moveDir != Vector3.zero);
    }
    void Dodge()
    {
        if (isDodge && dodgeAble)
        {
            StartCoroutine(Dash());
        }
    }
    IEnumerator Dash()
    {
        dodgeAble = false;
        ani.SetTrigger("Dodge");
        float dashSpeed = 3f;
        rigid.velocity *= dashSpeed;
        yield return waitDash; // 0.4sec
        isDodge = false;
        rigid.velocity = Vector3.zero;
        yield return waitDodge;
        dodgeAble = true;
    }
}
