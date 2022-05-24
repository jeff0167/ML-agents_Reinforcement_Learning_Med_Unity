using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentManager : MonoBehaviour
{
    [SerializeField]
    int totalSteps, agentAmount, evolutionPoints, generations;

    [SerializeField]
    float MoveSpeed;

    [SerializeField]
    GameObject agent, goalPoint;

    List<DarwinAgent> agents;

    [SerializeField]
    GameObject TopAgent;

    [SerializeField]
    TMPro.TextMeshProUGUI GenerationCount, SuccesfuldGeneration;

    bool isTraining, goalAchieved = false;

    float ClosestDistance = 100;

    void Start()
    {
        agents = new List<DarwinAgent>();
        for (int i = 0; i < agentAmount; i++)
        {
            DarwinAgent CurrentAgent = Instantiate(agent, new Vector2(0, 9), Quaternion.identity).GetComponent<DarwinAgent>();

            for (int j = 0; j < totalSteps; j++)
            {
                CurrentAgent.AddNewStep();
            }
            agents.Add(CurrentAgent);
        }
        TrainAgents();
    }

    void UpdateGenerationCount()
    {
        generations++;
        GenerationCount.text = generations.ToString();
    }

    void TrainAgents()
    {
        isTraining = true;
        StopAllCoroutines();
        StartCoroutine(PerformAgentSteps());
    }

    void NextStep()
    {
        if (!isTraining)
        {
            Evolve();
            TrainAgents();
        }
    }

    void Evolve()
    {
        if (ClosestDistance < 0.2f)
        {
            if (!goalAchieved)
            {
                goalAchieved = true;
                SuccesfuldGeneration.text = generations.ToString();
            }
            totalSteps--;
        }

        List<Vector2> BestSteps = TopAgent.GetComponent<DarwinAgent>().GetSteps();
        foreach (var item in agents)
        {
            item.AddEvolutionSteps(BestSteps, evolutionPoints);
        }
    }

    IEnumerator PerformAgentSteps()
    {
        int currentStep = 0;
        while (currentStep <= totalSteps - 1)
        {
            foreach (var agent in agents)
            {
                agent.transform.position += (Vector3)agent.GetStepByIndex(currentStep) * MoveSpeed;
            }
            currentStep++;
            yield return new WaitForEndOfFrame();
        }
        FinishTrainingStep();
    }

    void FinishTrainingStep()
    {
        GetBestAgent();
        foreach (var item in agents)
        {
            item.ResetToStart();
        }
        UpdateGenerationCount();
        isTraining = false;
        NextStep();
    }


    void GetBestAgent()
    {
        int agentId = 0;
        float minDistance = 100;
        Vector2 goal = goalPoint.transform.position;
        float BestDistance = 0;

        for (int i = 0; i < agents.Count; i++)
        {
            BestDistance = Vector2.Distance(agents[i].transform.position, goal);
            if (BestDistance < minDistance)
            {
                agentId = i;
                minDistance = BestDistance;
            }
        }
        TopAgent = agents[agentId].gameObject;
        ClosestDistance = minDistance;
        Debug.Log(minDistance);
    }
}
