using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    int score, total, highscore;

    [SerializeField]
    FlappyAgent agent;

    TMPro.TextMeshProUGUI scoreText;

    [SerializeField]
    TMPro.TextMeshProUGUI totalText, highscoreText;


    void Start()
    {
        highscore = 0;
        scoreText = GetComponent<TMPro.TextMeshProUGUI>();
    }

    public void ResetScore()
    {
        if(score > highscore)
        {
            highscore = score;
            highscoreText.text = highscore.ToString();
        }

        score = 0;
        scoreText.text = score.ToString();
    }

    public void IncreaseScore()
    {
        score++;
        total++;
        scoreText.text = score.ToString();
        totalText.text = total.ToString();

        agent.CheckPointReached();
    }
}
