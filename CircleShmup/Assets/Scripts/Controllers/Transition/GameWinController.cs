using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Manages the game win transition
 * @class GameWinController
 */
public class GameWinController : ASelect
{
    /**
     * Constructs base class
     */
    private new void Start()
    {
        base.Start();
    }

    /**
     * Loads the game win scene
     */
    public void GameWin()
    {
        StartCoroutine(LoadYourAsyncScene("End/Victory"));
    }
}
