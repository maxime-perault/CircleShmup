﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Controls the behaviour of the player
 * @class PlayerController
 */
[RequireComponent(
   typeof(Rigidbody2D),
   typeof(PolygonCollider2D),
   typeof(PlayerInputController))]
public class PlayerController : Entity
{
    public  bool                   paused;
    public  bool                   slide;
    public  Vector2                speed;
    public  PlayerSphereController sphereController;

    private Rigidbody2D            body2D;
    private PolygonCollider2D      polygonCollider2D;
    private PlayerInputController  inputController;

    private GameObject musicPlayer;
    private bool playerJustStartedToMove;
    private bool playerJustStoppedToMove;

    /**
     * Initializes the player controller by buffering 
     * all needed components
     */
    void Start()
    {
        body2D            = GetComponent<Rigidbody2D>();
        polygonCollider2D = GetComponent<PolygonCollider2D>();
        inputController   = GetComponent<PlayerInputController>();

        paused = false;
        playerJustStartedToMove = false;
        playerJustStoppedToMove = true;

        musicPlayer = GameObject.Find("MusicPlayer");
    }

    /**
     * Updates the player states
     */
    void Update()
    {
        if(paused)
        {
            return;
        }

        Vector2 axis = inputController.GetAxis();
        body2D.AddForce(new Vector2(speed.x * axis.x, speed.y * axis.y));


        // Animator hooks :)
        if(axis.x > 0)
        {
            // The player is going on the right
        }
        else if(axis.x < 0)
        {
            // The player is going on the left
        }
        
        if(axis.y > 0)
        {
            // The player is going up
        }
        else if(axis.y < 0)
        {
            // The player is going down
        }
        //

        if (axis.x == 0.0f && axis.y == 0.0f)
        {
            if (!slide)
            {
                body2D.velocity = new Vector2(0.0f, 0.0f);
            }

            if(!playerJustStoppedToMove)
            {
                OnPlayerJustStoppedToMove();
                playerJustStoppedToMove = true;
                playerJustStartedToMove = false;
            }
        }
        else
        {
            if(!playerJustStartedToMove)
            {
                OnPlayerJustStartedToMove();
                playerJustStartedToMove = true;
                playerJustStoppedToMove = false;
            }
        }

        if (inputController.IsAddingSphere())
        {
            sphereController.AddSphere();
        }
        else if (inputController.IsRemovingSphere())
        {
            sphereController.RemoveSphere();
        }

        bool bIncreaseRadius = false;
        if (inputController.IsClockwiseRotation())
        {
            bIncreaseRadius = true;
            sphereController.ReverseClockwise();
        }
        else if (inputController.IsCounterClockwiseRotation())
        {
            bIncreaseRadius = true;
            sphereController.ReverseCounterClockwise();
        }

        if(bIncreaseRadius)
        {
            sphereController.IncreaseRadius();
        }
        else
        {
            sphereController.DecreaseRadius();
        }
    }

    /**
     * TODO
     */
    private void OnPlayerJustStartedToMove()
    {
        AkSoundEngine.PostEvent("Beurre_Move", musicPlayer);
    }

    /**
     * TODO
     */
    private void OnPlayerJustStoppedToMove()
    {
        AkSoundEngine.PostEvent("Beurre_Stop", musicPlayer);
    }

    /**
     * TODO
     */
    public override void OnEntityDeath()
    {
        AkSoundEngine.PostEvent("Beurre_Death", musicPlayer);

        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.OnGameOver();
    }

    public override void OnHit(int hitPoint)
    {
        AkSoundEngine.PostEvent("Beurre_Hit", musicPlayer);
    }

    /**
     * Called when the game is paused
     */
    public void OnGamePaused()
    {
        paused = true;
        sphereController.OnGamePaused();
    }

    /**
     * Called when the game resumes
     */
    public void OnGameResumed()
    {
        paused = false;
        sphereController.OnGameResumed();
    }
}
