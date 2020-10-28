using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSession : MonoBehaviour
{
    // config params
    [Range(0.1f, 10f)] [SerializeField] float gameSpeed = 1f;
    [SerializeField] TextMeshProUGUI blockScoreOut;
    [SerializeField] TextMeshProUGUI bonusOut;
    [SerializeField] TextMeshProUGUI ballCountOut;
    [SerializeField] TextMeshProUGUI bonusGoalOut;
    [SerializeField] AudioClip bonusBallSFX;
    [SerializeField] GameObject bonusBallVFX;
    [SerializeField] bool isAutoPlayEnabled;
    [SerializeField] GameObject bonusPosition;

    // cahced reference
    [SerializeField] SceneLoader sceneloader;

    // state variables
    int blocksDestroyed = 0;
    int ballCount = 1;
    int levelFactor;
    int bonusBlocks = 0;
    int bonusGoal = 0;

    private void Awake()
    {
        // make GameStatus a singleton
        int gameStatusCount = FindObjectsOfType<GameSession>().Length;
        if (gameStatusCount > 1)
        {
            DestroySelf();
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        sceneloader = FindObjectOfType<SceneLoader>();
        levelFactor = sceneloader.GetLevelFactor();

        blockScoreOut.text = "0 x " + levelFactor.ToString() + " = 0";
        bonusBlocks = 0;
        bonusGoal = levelFactor;
        bonusGoalOut.text = bonusGoal.ToString();
        bonusOut.text = bonusBlocks.ToString() + " x " + levelFactor.ToString() + " = " + (bonusBlocks * levelFactor).ToString();

        ShowBallsLeft();

    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = gameSpeed;
    }

    public int LoseBall()
    {

        ballCount--;

        ShowBallsLeft();

        return ballCount;
    }

    public void AddBall()
    {
        ballCount++;

        ShowBallsLeft();

    }

    public void ShowBallsLeft()
    {
        string textOut = " ";

        for (int i = 0; i < ballCount; i++)
        {
            textOut = textOut + "O ";
        }

        ballCountOut.text = textOut;
    }

    public void AddToScore()
    {
        int currentScore;

        blocksDestroyed ++;

        currentScore = blocksDestroyed * levelFactor;

        blockScoreOut.text = blocksDestroyed.ToString() + " x " + levelFactor.ToString() + " = " + currentScore.ToString();

    }

    public void AddToBonus()
    {
        bonusBlocks ++;

        bonusOut.text = bonusBlocks.ToString() + " x " + levelFactor.ToString() + " = " + (bonusBlocks * levelFactor).ToString();

        if ((bonusBlocks * levelFactor) >= bonusGoal)
        {
            HitBonusGoal();
        }
    }

    public int GetFinalScore()
    {
        return (blocksDestroyed * levelFactor);
    }

    public void DestroySelf()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    public void HitBonusGoal()
    {
        bonusBlocks = 0;
        bonusGoal += levelFactor;
        AddBall();

        PlayBonusBallSFX();
        TriggerBonusBallVFX();
        bonusOut.text = bonusBlocks.ToString() + " x " + levelFactor.ToString() + " = " + (bonusBlocks * levelFactor).ToString();
        bonusGoalOut.text = bonusGoal.ToString();

    }

    public bool IsAutoPlayEnabled()
    {
        return isAutoPlayEnabled;
    }

    private void PlayBonusBallSFX()
    {
        AudioSource.PlayClipAtPoint(bonusBallSFX, Camera.main.transform.position);
    }

    private void TriggerBonusBallVFX()
    {


        GameObject sparkles = Instantiate(bonusBallVFX, bonusPosition.transform.position, bonusPosition.transform.rotation);
        Destroy(sparkles, 1f);

    }

}
