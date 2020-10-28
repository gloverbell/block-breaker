using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Block : MonoBehaviour
{
    // config parameters
    [SerializeField] TextMeshProUGUI blockText;
    [SerializeField] AudioClip blockHitSFX;
    [SerializeField] GameObject blockHitVFX;

    // cahced reference
    Level level;
    GameSession gameSession;

    // state variables
    int blockFactor;
    int levelFactor;

    private void Start()
    {
        level = FindObjectOfType<Level>();
        levelFactor = level.GetLevelFactor();

        gameSession = FindObjectOfType<GameSession>();

        if (tag == "Breakable")
        {
            level.CountBlocks();

            blockFactor = Random.Range(1, 10) * levelFactor;

        }
        else if (tag == "Unbreakable")
        {
            blockFactor = levelFactor;
        }

        blockText.text = blockFactor.ToString();
             

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (tag == "Breakable")
        {
            blockFactor = blockFactor - levelFactor;
            if (blockFactor <= 0)
            {
                DestroyBlock();
            }
        }
        else if (tag == "Unbreakable")
        {
            HitBonusBlock();
        }

        blockText.text = blockFactor.ToString();
    }

    private void HitBonusBlock()
    {
        gameSession.AddToBonus();

        PlayBlockHitSFX();
        TriggerBlockHitVFX();
    }

    private void DestroyBlock()
    {
        gameSession.AddToScore();

        PlayBlockHitSFX();
        TriggerBlockHitVFX();

        Destroy(gameObject);
        level.BlockDestroyed();
    }

    private void PlayBlockHitSFX()
    {
        AudioSource.PlayClipAtPoint(blockHitSFX, Camera.main.transform.position);
    }

    private void TriggerBlockHitVFX()
    {
        GameObject sparkles = Instantiate(blockHitVFX, transform.position, transform.rotation);
        Destroy(sparkles, 1f);

    }
}
