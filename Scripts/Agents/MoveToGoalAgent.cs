using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class MoveToGoalAgent : Agent
{
    [SerializeField] private GameObject target;
    [SerializeField] private float MoveSpeed;
    [SerializeField] private Material winMaterial;
    [SerializeField] private Material loseMaterial;
    [SerializeField] private MeshRenderer floorMeshRenderer;

    [SerializeField][Range(1,10)]
    float Dificulty;

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(target.transform.localPosition);
    }

    //public override void OnEpisodeBegin()
    //{
    //    Vector3 agentPos = new Vector3(Random.Range(4.0f, -4.0f), 0.1188646f, Random.Range(4.0f, -4.0f));
    //    transform.localPosition = agentPos;
    //    Vector2 randomCirclePos = Random.insideUnitCircle * Dificulty;
    //    Vector3 targetPos = new Vector3(randomCirclePos.x, 0.05418563f, randomCirclePos.y) + agentPos;
    //    target.transform.localPosition = targetPos;
    //}

    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];

        transform.localPosition += new Vector3(moveX, 0, moveZ).normalized * Time.deltaTime * MoveSpeed;   // normalize for game play test

        AddReward(-1 / 100);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> yo = actionsOut.ContinuousActions;
        yo[0] = Input.GetAxisRaw("Horizontal"); // magnets how do they work
        yo[1] = Input.GetAxisRaw("Vertical");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Goal"))
        {
            SetReward(+1f);
            floorMeshRenderer.material = winMaterial;
            EndEpisode();
        }
        if (other.gameObject.CompareTag("Wall"))
        {
            SetReward(-1f);
            floorMeshRenderer.material = loseMaterial;
            EndEpisode();
        }
    }
}
