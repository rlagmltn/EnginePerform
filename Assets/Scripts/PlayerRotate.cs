using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    Camera cam;
    [SerializeField]
    GameObject cube;

    Vector3 mousePos = Vector3.zero;
    Vector3 LookPos = Vector3.zero;
    private void Awake()
    {
        transform.rotation = Quaternion.identity;
        cam = Camera.main;
    }
    void Update()
    {
        RayCastingMousePosition();
        LookPos = new Vector3(mousePos.x, 4f, mousePos.z);
        Debug.Log(LookPos);
        transform.LookAt(LookPos);
        cube.transform.position = LookPos;
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
}
