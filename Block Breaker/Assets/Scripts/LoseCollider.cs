using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoseCollider : MonoBehaviour
{
    // cached reference
    GameSession gameSession;
    Ball ball;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameSession = FindObjectOfType<GameSession>();

        if (gameSession.LoseBall() <= 0)
        {
            LoadGameOverScene();
        }
        else
        {
            ball = FindObjectOfType<Ball>();
            ball.RestartBall();
        }
    }

    public void LoadGameOverScene()
    {

        SceneManager.LoadScene("Game Over");
    }

}
