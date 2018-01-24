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
        // TODO
    }

    /**
     * Display a score at the top of an enemy
     * @param score The score to display / add
     */
    public static void AddScore(int score)
    {
        // GameObject score = Instantiate(instance.messagePrefab, instance.parent.transform);

        // Text text = go.GetComponent<Text>();
        // text.text = message;
        // 
        // Destroy(go, duration);
        // return go;
    }
}
