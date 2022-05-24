using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeManager : MonoBehaviour
{
    [SerializeField]
    GameObject Pipe;

    [SerializeField]
    float moveSpeed, dificulty, randomOffsetG, spawnRate;
    float offset = 5;


    List<GameObject> pipes;

    private void Awake()
    {
        pipes = new List<GameObject>();
        ResetScence();
    }

    public void ResetScence()
    {
        if (pipes.Count != 0)
        {
            foreach (var item in pipes)
            {
                Destroy(item);
            }
            pipes.Clear();
        }
        if (IsInvoking()) CancelInvoke();
        InvokeRepeating("DifficultySpawner", 0, spawnRate);
    }

    public void DifficultySpawner()
    {
        GameObject g = Instantiate(Pipe, new Vector2(15, 0), Quaternion.identity);
        g.GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, 0);
        pipes.Add(g);

        GameObject pipe1 = g.transform.GetChild(0).gameObject;
        GameObject pipe2 = g.transform.GetChild(1).gameObject;

        float randomOffset = Random.Range(-randomOffsetG, randomOffsetG);
        float random1 = Random.Range(12.0f, 9.0f) + offset + dificulty;

        pipe1.transform.localPosition = new Vector3(0, -random1 + randomOffset, 0);
        pipe2.transform.localPosition = new Vector3(0, random1 + randomOffset, 0);
    }

    public void RemovePipe(GameObject pipe)
    {
        pipes.Remove(pipe);
    }

    public float GetClosestPipe()
    {
        float index = 100;

        if(pipes == null) return 0;

        foreach (var item in pipes)
        {
            if (item.transform.position.x < index && item.transform.position.x > -0.01f)
            {
                index = item.transform.position.x;
            }
        }
        return index;
    }
}
