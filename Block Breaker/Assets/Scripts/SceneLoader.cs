using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    // config params
    [SerializeField] Slider factorSlider;
    [SerializeField] int levelFactor = 3;

    // cahced reference
    GameSession gameSession;

    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (levelFactor == 0)
        {
            // Get selected level factor from start scene
            factorSlider = FindObjectOfType<Slider>();
            levelFactor = (int) factorSlider.value;

        }

        if (currentSceneIndex < 4)
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
    }

    public void LoadStartScene()
    {

        gameSession = FindObjectOfType<GameSession>();
        gameSession.DestroySelf();

        SceneManager.LoadScene(0);
    }

    public int GetLevelFactor()
    {
        return levelFactor;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}