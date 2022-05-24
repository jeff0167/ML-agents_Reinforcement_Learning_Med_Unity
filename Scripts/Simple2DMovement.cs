using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simple2DMovement : MonoBehaviour
{
    float h, v;
    [Range(1,100)]
    public float moveSpeed;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(h, v).normalized * moveSpeed;
    }
}
