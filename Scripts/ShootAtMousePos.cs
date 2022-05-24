using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using UnityEngine;

public class ShootAtMousePos : MonoBehaviour
{
    [SerializeField]
    GameObject projectile;
    Camera cam;

    void Start()
    {
        cam = Camera.main;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        var d = cam.ScreenPointToRay(Input.mousePosition);
        Debug.Log(d);
        Vector3 dir = Vector3.zero;

        if (Physics.Raycast(d, out RaycastHit raycastHit))
        {
            dir = raycastHit.point - transform.position;
        }

        var rot = Quaternion.LookRotation(dir);


        Instantiate(projectile, transform.position + dir.normalized * 2, rot);
        //Instantiate(projectile, transform.position + dir.normalized * 3, rot);
    }
}
