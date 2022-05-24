using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarwinAgent : MonoBehaviour
{
    List<Vector2> steps;

    bool Instantiated = false;

    Vector2 StartPos;

    public void Awake()
    {
        StartPos = transform.position;
    }

    public void AddNewStep()
    {
        if (!Instantiated)
        {
            steps = new List<Vector2>();
            Instantiated = true;
        }

        steps.Add(NewStep());
    }

    Vector2 NewStep()
    {
        return new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
    }

    public void AddEvolutionSteps(List<Vector2> parentSteps, int exploration = 10)
    {
        steps = new List<Vector2>();
        List<int> index = new List<int>();  

        for (int i = 0; i < parentSteps.Count; i++)
        {
            index.Add(i);
            steps.Add(parentSteps[i]);
        }


        for (int i = 0; i < exploration; i++)
        {
            int randomindex = Random.Range(0, steps.Count);


            foreach (var item in index)
            {
                if(item == randomindex)
                {
                    index.Remove(item);
                    steps[randomindex] = NewStep();
                    break;
                }
            }

           // steps[randomindex] = NewStep();
        }
    }

    public List<Vector2> GetSteps()
    {
        return steps;
    }

    public void ResetToStart()
    {
        transform.position = StartPos;
    }

    public Vector2 GetStepByIndex(int index)
    {
        return steps[index];
    }
}
