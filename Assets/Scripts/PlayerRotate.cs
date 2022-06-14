using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    Camera cam;
    [SerializeField]
    GameObject cursor;

    Vector3 mousePos = Vector3.zero;
    public Vector3 lookPos = Vector3.zero;
    private void Awake()
    {
        transform.rotation = Quaternion.identity;
        cam = Camera.main;
    }
    void Update()
    {
        RayCastingMousePosition();
        lookPos = new Vector3(mousePos.x, 0.2f, mousePos.z);
        Debug.Log(lookPos);
        transform.LookAt(lookPos);
        cursor.transform.position = lookPos;
    }
    void RayCastingMousePosition()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            //Debug.DrawRay(ray.origin, ray.direction * 20, Color.red, 5f);
            mousePos = hit.point;
        }
    }
}
