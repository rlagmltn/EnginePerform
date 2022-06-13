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

    bool isDodge;
    bool isWalk;
    bool dodgeAble = true;

    WaitForSeconds waitDodge = new WaitForSeconds(0.3f);
    Vector3 mousePos = Vector3.zero;
    Vector3 LookPos = Vector3.zero;
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
    }
    private float movespeed = 10f;
    void Move()
    {
        #region Dodge
        if (!dodgeAble)
        {
            rigid.velocity = Vector3.zero;
            return;
        }
        if (isDodge)
        {
            Dodge();
        }
        #endregion
        RayCastingMousePosition();

        LookPos = new Vector3(mousePos.x + transform.position.x, 2f, mousePos.z + transform.position.z);
        Debug.Log(LookPos);
        transform.LookAt(LookPos);
        cube.transform.position = LookPos;
        Vector3 moveDir = GetInput();
        rigid.velocity = moveDir;

        ani.SetBool("isRun", moveDir != Vector3.zero);
        //ani.SetBool("isWalk", isWalk);
    }
    Vector3 GetInput()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        isWalk = Input.GetButton("Walk");
        isDodge = Input.GetButton("Dodge");

        return new Vector3(x, 0f, z) * movespeed;
    }
    void RayCastingMousePosition()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            Debug.DrawRay(ray.origin, ray.direction * 20, Color.red, 5f);
            mousePos = hit.point;
        }
    }
    void Dodge()
    {
        StartCoroutine(DodgeMove());
    }
    IEnumerator DodgeMove()
    {
        dodgeAble = false;
        ani.SetTrigger("Dodge");

        yield return waitDodge;
        dodgeAble = true;
    }
}
