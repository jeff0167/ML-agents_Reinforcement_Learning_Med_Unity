using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MoveForward : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField]
    float moveSpeed;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * moveSpeed;
    }
}
