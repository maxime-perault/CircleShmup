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
    public  GameObject          messagePrefab;
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
        AddScore(999, Vector3.zero);
    }

    /**
     * Display a score at the top of an enemy
     * @param score The score to display / add
     */
    public static void AddScore(int score, Vector3 position)
    {
        position.x -= 0.1f;
        position.y += 0.5f;

        GameObject go = Instantiate(managerInstance.messagePrefab, managerInstance.transform);
        go.transform.position = position;

        Score scoreComponent = go.GetComponent<Score>();
        scoreComponent.SetScore(score);
    }
}
