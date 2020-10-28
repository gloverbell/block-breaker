using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    // config params
    [SerializeField] float screenWidthInUnits = 16f;
    [SerializeField] float minX= 1f;
    [SerializeField] float maxX = 15f;

    // cached references
    GameSession gameSession;
    Ball ball;

    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        ball = FindObjectOfType<Ball>();
    }

    void Update()
    {

        Vector2 paddlePos = new Vector2(transform.position.x, transform.position.y);
        paddlePos.x = Mathf.Clamp(GetXPos(), minX, maxX);

        transform.position = paddlePos;
    }

    private float GetXPos()
    {
        if (gameSession.IsAutoPlayEnabled())
        {

            return ball.transform.position.x;
        }
        else
        {
            return (Input.mousePosition.x / Screen.width) * screenWidthInUnits;
        }

    }
}
