using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class FlappyAgent : Agent
{
    [SerializeField]
    FlappingBird bird;
    Rigidbody2D rb;

    PipeManager pipeManager;

    Vector2 startPos;

    void Start()
    {
        pipeManager = Camera.main.GetComponent<PipeManager>();
        rb = bird.gameObject.GetComponent<Rigidbody2D>();
        startPos = bird.transform.position;
    }
    public override void OnEpisodeBegin()
    {
       // Debug.Log("Begin episode");
        bird.transform.position = startPos;
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<int> discreteActions = actionsOut.DiscreteActions;
        discreteActions[0] = Input.GetKey(KeyCode.Space) ? 1 : 0;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        //float worldHeight = 100f;
        //float birdNormalizedY = (bird.transform.position.y + (worldHeight / 2f)) / worldHeight;
        //sensor.AddObservation(birdNormalizedY);

        sensor.AddObservation(transform.position.y);

        float pipeX = pipeManager.GetClosestPipe() / 100f;
        sensor.AddObservation(pipeX);

        sensor.AddObservation(rb.velocity.y);

        AddReward(1f / 1000);
    }

    public void Die()
    {
        AddReward(-1);
    }

    public void CheckPointReached()
    {
        //Debug.Log("ReachedCheckPoint");
        AddReward(1);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        if(actions.DiscreteActions[0] == 1)
        {
            bird.Flap();
        }
    }
}
