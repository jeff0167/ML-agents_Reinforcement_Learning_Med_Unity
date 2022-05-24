using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class DodgeAgent : Agent
{
    Rigidbody rb;
    Vector2 startPos;

    [SerializeField]
    float MoveSpeed;

    [SerializeField]
    BulletShooterManager shooterManager;

    void Start()
    {
        startPos = transform.localPosition;
        rb = GetComponent<Rigidbody>();
    }
    //public override void OnEpisodeBegin() // for training
    //{
    //    rb.velocity = Vector3.zero;

    //    Vector2 randomCirclePos = Random.insideUnitCircle * 4;
    //    Vector2 randPos = new Vector3(randomCirclePos.x, 0, randomCirclePos.y);

    //    transform.localPosition = startPos + randPos;
    //}

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<int> discreteActions = actionsOut.DiscreteActions;

        int x = (int)Input.GetAxisRaw("Horizontal");
        int z = (int)Input.GetAxisRaw("Vertical");

        int moveX = 0, moveZ = 0;

        switch (x)
        {
            case -1: moveX = 0; break;
            case 0: moveX = 1; break;
            case 1: moveX = 2; break;
        }

        switch (z)
        {
            case -1: moveZ = 0; break;
            case 0: moveZ = 1; break;
            case 1: moveZ = 2; break;
        }

        discreteActions[0] = moveX;
        discreteActions[1] = moveZ;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.position.x);
        sensor.AddObservation(transform.position.z);

        //sensor.AddObservation(rb.velocity.x);
        //sensor.AddObservation(rb.velocity.z);

        AddReward(1f / 50);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        int x = actions.DiscreteActions[0];
        int z = actions.DiscreteActions[1];

        int moveX = 0, moveZ = 0;

        switch (x)
        {
            case 0: moveX = -1; break;
            case 1: moveX = 0; break;
            case 2: moveX = 1; break;
        }

        switch (z)
        {
            case 0: moveZ = -1; break;
            case 1: moveZ = 0; break;
            case 2: moveZ = 1; break;
        }

        rb.velocity = new Vector3(moveX, 0, moveZ).normalized * MoveSpeed;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bullet") || other.gameObject.CompareTag("Wall"))
        {
            AddReward(-1);
           // shooterManager.DestroyBullets();   // for training
            Debug.Log("Got hit og wall");
            EndEpisode();
        }
    }

    //private void OnCollisionStay(Collision other)
    //{
    //    if (other.gameObject.CompareTag("Wall"))
    //    {
    //        AddReward(-1f);
    //        shooterManager.DestroyBullets();
    //        EndEpisode();
    //        //AddReward(-0.1f);
    //        Debug.Log("Wall");
    //    }
    //}
}
