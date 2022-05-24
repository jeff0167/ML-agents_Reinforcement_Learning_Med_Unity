using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Simple3DMovement : MonoBehaviour
{
    Rigidbody rb;
    float h, v;

    [SerializeField]
    float MoveSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(h, 0, v).normalized * MoveSpeed;
    }
}
