using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody rigid;
    Animator ani;
    Camera cam;
    [SerializeField]
    GameObject cube;

    bool isDodge = false;
    bool isWalk;
    bool dodgeAble = true;
    private float movespeed = 15f;

    WaitForSeconds waitDodge = new WaitForSeconds(1f);
    WaitForSeconds waitDash = new WaitForSeconds(0.2f);
    Vector3 mousePos = Vector3.zero;
    Vector3 lookPos = Vector3.zero;
    Vector3 moveDir = Vector3.zero;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        ani = GetComponentInChildren<Animator>();
        cam = Camera.main;
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
        moveDir = new Vector3(x, 0f, z) * movespeed;
        isWalk = Input.GetButton("Walk");
        if (dodgeAble) isDodge = Input.GetButtonDown("Dodge");
    }
    void Move()
    {
        RayCastingMousePosition();
        lookPos = new Vector3(mousePos.x, 2f, mousePos.z);
        transform.LookAt(lookPos);
        cube.transform.position = lookPos;
        if (!isDodge) rigid.velocity = moveDir;
        ani.SetBool("isRun", moveDir != Vector3.zero);
        //ani.SetBool("isWalk", isWalk);
    }
    void RayCastingMousePosition()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            mousePos = hit.point;
        }
        Debug.DrawRay(transform.position, (lookPos - transform.position).normalized * 50, Color.red);
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
        Debug.Log("Dodge!!");
        dodgeAble = false;
        ani.SetTrigger("Dodge");
        Vector3 dashPos = new Vector3(lookPos.x - transform.position.x, 0f, lookPos.z - transform.position.x).normalized;
        float dashSpeed = movespeed * 2f;
        rigid.velocity = dashSpeed * dashPos;
        yield return waitDash; // 0.4sec
        isDodge = false;
        rigid.velocity = Vector3.zero;
        yield return waitDodge;
        dodgeAble = true;
    }
}
