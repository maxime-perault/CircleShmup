using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * The base enemy class
 * @class Enemy
 */
public class Enemy : Entity
{
    public int        bufferIndex;
    public WaveHandle handle;

    /**
     * Called when the game is paused
     */
    public virtual void OnGamePaused()
    {
        // None
    }

    /**
     * Called when the game resumes
     */
    public virtual void OnGameResumed()
    {
        // None
    }
}