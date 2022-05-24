using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
public class RockPaperScissorsAgent : Agent
{
    [SerializeField]
    TMPro.TextMeshProUGUI AI_AttackText, Player_AttackText, winText;

    enum RockPaperScissors
    {
        rock,
        paper,
        scissors
    }

    RockPaperScissors AI_Chossen, PlayerChossen;

    private void Start() // must manualy start it for heuristic
    {
        OnEpisodeBegin();
    }

    public override void OnEpisodeBegin() // for training
    {
        int r = Random.Range(0, 3);

        PlayerChossen = (RockPaperScissors)r;

       // DisplayResults();

        //RequestAction();
    }


    //private void Update()
    //{
    //    MadeUpHeuristics();
    //}

    //public void MadeUpHeuristics()
    //{
    //    bool HasChosen = false;

    //    if (Input.GetKeyDown(KeyCode.R))
    //    {
    //        AI_Chossen = (RockPaperScissors)0;
    //        HasChosen = true;
    //    }
    //    if (Input.GetKeyDown(KeyCode.P))
    //    {
    //        AI_Chossen = (RockPaperScissors)1;
    //        HasChosen = true;
    //    }
    //    if (Input.GetKeyDown(KeyCode.S))
    //    {
    //        AI_Chossen = (RockPaperScissors)2;
    //        HasChosen = true;
    //    }

    //    if (HasChosen)
    //    {
    //        Debug.Log("Choose an action");
    //        RequestAction();
    //        HasChosen = false;
    //    }
    //}

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        Debug.Log("Yo");
       // base.Heuristic(actionsOut);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation((int)PlayerChossen);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        int action = actions.DiscreteActions[0];
        AI_Chossen = (RockPaperScissors)action;


        //Debug.Log("AI chose " + action);

       // DisplayResults();

        CheckWin();
    }

    void DisplayResults()
    {
        AI_AttackText.text = AI_Chossen.ToString();
        Player_AttackText.text = PlayerChossen.ToString();

        //Debug.Log("AI chose: " + AI_Chossen.ToString());
        //Debug.Log("Player chose: " + PlayerChossen.ToString());
    }


    void CheckWin()
    {
        int ai = ((int)AI_Chossen);
        int player = ((int)PlayerChossen);


        if (AI_Chossen == PlayerChossen)
        {
            winText.text = "Draw";
            Draw();
        }
        else if (ai == player - 1 || ((ai == 2) && (player == 0)))            // ai chose 0 = rock    player choose 2 = scissor
        {
            winText.text = "AI lost";
            Lose();
        }
        else
        {
            winText.text = "AI won";
            Won();
        }
        EndEpisode();
    }

    void Won()
    {
        AddReward(1);
    }

    void Lose()
    {
        AddReward(-1);
    }

    void Draw()
    {
        AddReward(0.1f);
    }
}
