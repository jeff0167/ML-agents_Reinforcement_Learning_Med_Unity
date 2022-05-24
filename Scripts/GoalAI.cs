using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalAI : MonoBehaviour
{
    [SerializeField]
    GameObject Target;

    [SerializeField]
    float MoveSpeed;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 targetDir = Target.transform.position - transform.position;
        rb.velocity = targetDir.normalized * MoveSpeed;
    }
}
