using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/**
 * This manager can display messages to the player
 * @class MessageManager
 */
public class ScoreManager : MonoBehaviour
{
    private int                 managerScore;
    public  GameObject          messagePrefab;
    public  Text                managerUiScoreText;
    private static ScoreManager managerInstance;
    
    /**
     * Called once at loading time
     */
    void Awake()
    {
        managerInstance = this;
    }

    /**
     * TODO
     */
    void Start()
    {
        managerScore = 0;
    }

    /**
     * Display a score at the top of an enemy
     * @param score The score to display / add
     */
    public static void AddScore(int score, Vector3 position)
    {
        if(managerInstance == null)
        {
            Debug.Log("Did you add the score manager to the scene ?");
            return;
        }

        position.x -= 0.1f;
        position.y += 0.2f;

        GameObject go = Instantiate(managerInstance.messagePrefab, managerInstance.transform);
        go.transform.position = position;

        Score scoreComponent = go.GetComponent<Score>();
        scoreComponent.SetScore(score);

        managerInstance.managerScore += score;
        GameObject.Find("GameManager").GetComponent<GameManager>().currentScore += score;
        managerInstance.managerUiScoreText.text = managerInstance.managerScore.ToString();
    }

    /**
     * Returns the current score
     */
    public static int GetScore()
    {
        return managerInstance.managerScore;
    }
}
