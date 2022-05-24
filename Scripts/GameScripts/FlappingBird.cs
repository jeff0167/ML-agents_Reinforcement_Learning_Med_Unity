using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlappingBird : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField]
    float FlapMagnitude;

    [SerializeField]
    Score Score;

    PipeManager pipeManager;
    FlappyAgent FlappyAgent;

    void Start()
    {
        FlappyAgent = GetComponent<FlappyAgent>();  
        pipeManager = Camera.main.GetComponent<PipeManager>();
        rb = GetComponent<Rigidbody2D>();
    }

    //void Update()
    //{
    //    //if (Input.GetKey(KeyCode.Space))
    //    //{
    //    //    Flap();
    //    //}
    //}

    public void Flap()
    {
        rb.velocity = Vector3.zero;
        rb.AddForce(new Vector2(0, FlapMagnitude));
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        pipeManager.ResetScence();
        Score.ResetScore();
        FlappyAgent.Die();
        FlappyAgent.EndEpisode();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("CheckPoint"))
        {
            Score.IncreaseScore();
            return;
        }
    }
}
