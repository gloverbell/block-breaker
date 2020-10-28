using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Level : MonoBehaviour
{
    // config params
    [SerializeField] int blocks;
    [SerializeField] TextMeshProUGUI levelFactorOut;
    int levelFactor;

    // cached references
    [SerializeField] SceneLoader sceneloader;


    public void Start()
    {
        sceneloader = FindObjectOfType<SceneLoader>();
        levelFactor = sceneloader.GetLevelFactor();

        levelFactorOut.text = levelFactor.ToString();
    }

    public void CountBlocks()
    {
        blocks++;

    }

    public void BlockDestroyed()
    {
        blocks--;

        if (blocks <= 0)
        {
            sceneloader.LoadNextScene();
        }
    }


    public int GetLevelFactor()
    {
        return levelFactor;
    }
}
