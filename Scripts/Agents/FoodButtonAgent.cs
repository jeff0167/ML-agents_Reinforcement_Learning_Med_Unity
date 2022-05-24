using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class FoodButtonAgent :Agent
{
    Rigidbody rb;

    [SerializeField]
    UnityEvent OnAteFood; 
    [SerializeField]
    UnityEvent OnFail;
    [SerializeField]
    UnityEvent OnEpisodeBeginEvent;

    [SerializeField]
    FoodButton foodButton;
    [SerializeField]
    FoodSpawner foodSpawner;

    [SerializeField] private GameObject food;
    [SerializeField] private GameObject Button;
    [SerializeField] private float MoveSpeed;

    bool Success = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    public override void OnEpisodeBegin()
    {
        Debug.Log("begin episode");

        if (!Success)
        {
            OnFail?.Invoke();
        }

        Success = false;

        transform.localPosition = new Vector3(-4, 0.5f, Random.Range(-3.5f, 3.5f));
        foodButton.ResetButton();
        foodSpawner.FoodDespawned();
        OnEpisodeBeginEvent?.Invoke();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(foodButton.CanUseButton ? 1 : 0);

        Vector3 dirToFoodButton = (foodButton.transform.position - transform.position).normalized;
        sensor.AddObservation(dirToFoodButton.x);
        sensor.AddObservation(dirToFoodButton.z);

        sensor.AddObservation(foodSpawner.HasFoodSpawned ? 1 : 0);

        if (foodSpawner.HasFoodSpawned)
        {
            Vector3 dirToFood = (food.transform.position - transform.position).normalized;
            sensor.AddObservation(dirToFood.x);
            sensor.AddObservation(dirToFood.z);
        }
        else
        {
            sensor.AddObservation(0f);
            sensor.AddObservation(0f);
        }
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.DiscreteActions[0];
        float moveZ = actions.DiscreteActions[1];

        Vector3 addForce = new Vector3();

        switch (moveX)
        {
            case 0: addForce.x = 0f; break;
            case 1: addForce.x = -1f; break;
            case 2: addForce.x = +1f; break; 
        } 
        switch (moveZ)
        {
            case 0: addForce.z = 0f; break;
            case 1: addForce.z = -1f; break;
            case 2: addForce.z = +1f; break; 
        }

        rb.velocity = addForce *  MoveSpeed;

        bool isUseButtonDown = actions.DiscreteActions[2] == 1;
        if (isUseButtonDown)
        {
            Collider[] colliders = Physics.OverlapBox(transform.position,Vector3.one * 0.5f);
            foreach (var item in colliders)
            {
                if(item.TryGetComponent<FoodButton>(out FoodButton foodButton))
                {
                    if (foodButton.CanUseButton)
                    {
                        foodButton.UseButton();
                        AddReward(1);
                    }
                }
            }
        }
        AddReward(-1f / 1000); // insert after it has succeded to maximize time to finish the task    
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<int> discreteActions = actionsOut.DiscreteActions;

        switch (Mathf.RoundToInt(Input.GetAxisRaw("Horizontal")))
        {
            case -1: discreteActions[0] = 1; break;
            case 0: discreteActions[0] = 0; break;
            case 1: discreteActions[0] = 2; break;
        }
        switch (Mathf.RoundToInt(Input.GetAxisRaw("Vertical")))
        {
            case -1: discreteActions[1] = 1; break;
            case 0: discreteActions[1] = 0; break;
            case 1: discreteActions[1] = 2; break;
        }
        discreteActions[2] = Input.GetKey(KeyCode.E) ? 1 : 0;
    }

    private void OnCollisionEnter(Collision other)
    {
        Colliding(other);
    }

    void Colliding(Collision other)
    {
        if (other.gameObject.TryGetComponent<Food>(out Food food)) 
        {
            AddReward(1f); // this ads reward while set reward will override aditional reward in this step
            food.EatFood();
            Success = true;
            OnAteFood?.Invoke();
            EndEpisode();
        }
        //if (other.gameObject.CompareTag("Wall"))
        //{
        //    AddReward(-1f);
        //    EndEpisode();
        //}
    }
}
