using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOver : MonoBehaviour
{
    // cached references
    GameSession gameSession;

    [SerializeField] TextMeshProUGUI finalScoreOut;

    // Start is called before the first frame update
    void Start()
    {
        gameSession= FindObjectOfType<GameSession>();
        finalScoreOut.text = gameSession.GetFinalScore().ToString();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
