using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Manages the game over transition
 * @class GameOverController
 */
public class GameOverController : ASelect
{
    /**
     * Constructs base class
     */
    private new void Start()
    {
        base.Start();
    }

    /**
     * Loads the game over scene
     */
    public void GameOver()
    {
        StartCoroutine(LoadYourAsyncScene("End/GameOver"));
    }
}
