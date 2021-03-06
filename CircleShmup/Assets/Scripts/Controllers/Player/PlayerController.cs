﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

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
    public  SpriteRenderer         playerStateRenderer;
    public  SpriteRenderer         playerSpriteRenderer;
    public  List<Sprite>           playerStateSprites = new List<Sprite>();

    private Rigidbody2D            body2D;
    private PolygonCollider2D      polygonCollider2D;
    private PlayerInputController  inputController;
    private ParticleSystem         particleSystem;

    private GameObject musicPlayer;
    private bool playerJustStartedToMove;
    private bool playerJustStoppedToMove;

    private PlayerAnimator Panimator;

    private ScreenShake screenshake;

    private SelectContinue scontinue;
    private bool isContinue = false;

    /**
     * Initializes the player controller by buffering 
     * all needed components
     */
    void Start()
    {
        body2D            = GetComponent<Rigidbody2D>();
        polygonCollider2D = GetComponent<PolygonCollider2D>();
        inputController   = GetComponent<PlayerInputController>();
        Panimator         = GetComponentInChildren<PlayerAnimator>();
        particleSystem    = GetComponentInChildren<ParticleSystem>();
        particleSystem.GetComponent<Renderer>().sortingLayerName = "UI";

        screenshake = GetComponent<ScreenShake>();

        paused = false;
        playerJustStartedToMove = false;
        playerJustStoppedToMove = true;

        musicPlayer = GameObject.Find("MusicPlayer");
        StartCoroutine("InvincibleBlinking");

        scontinue = GameObject.Find("Continue_script").GetComponent<SelectContinue>();
    }

    private void Update()
    {
        inputController.IsClockwiseRotation();
        inputController.IsCounterClockwiseRotation();
    }

    /**
     * Updates the player states
     */
    void FixedUpdate()
    {
        if ((isContinue == true) && (scontinue.isActive() == false))
        {
            OnGameResumed();
            isDead = false;
            isContinue = false;
        }
        if(paused)
        {
            return;
        }

        Vector2 axis = inputController.GetAxis();
        body2D.AddForce(new Vector2(speed.x * axis.x, speed.y * axis.y));


        // Animator hooks :)
        if(axis.x > 0)
        {
            Panimator.Right();
        }
        else if(axis.x < 0)
        {
            Panimator.Left();
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

        // Player life hook
        switch (hitPoint)
        {
            case 0: playerStateRenderer.sprite = playerStateSprites[0]; Panimator.SetLife(0); break;
            case 1: playerStateRenderer.sprite = playerStateSprites[1]; Panimator.SetLife(1); break;
            case 2: playerStateRenderer.sprite = playerStateSprites[2]; Panimator.SetLife(2); break;
            case 3: playerStateRenderer.sprite = playerStateSprites[3]; Panimator.SetLife(3); break;
            case 4: playerStateRenderer.sprite = playerStateSprites[4]; Panimator.SetLife(4); break;
            case 5: playerStateRenderer.sprite = playerStateSprites[5]; Panimator.SetLife(5); break;
            case 6: playerStateRenderer.sprite = playerStateSprites[5]; Panimator.SetLife(6); break;
            default: break;
        }
    }

    /**
     * TODO
     */
    private void OnPlayerJustStartedToMove()
    {
        if(MusicManager.WebGLBuildSupport)
        {
            MusicManager.PostEvent("Beurre_Move");
        }
        else
        {
            #if !UNITY_WEBGL
                AkSoundEngine.PostEvent("Beurre_Move", musicPlayer);
            #endif
        }

    }

    /**
     * TODO
     */
    private void OnPlayerJustStoppedToMove()
    {
        if (MusicManager.WebGLBuildSupport)
        {
            MusicManager.PostEvent("Beurre_Stop");
        }
        else
        {
            #if !UNITY_WEBGL
                AkSoundEngine.PostEvent("Beurre_Stop", musicPlayer);
            #endif 
        }  
    }

    /**
     * TODO
     */
    public override void OnEntityDeath()
    {
        if (MusicManager.WebGLBuildSupport)
        {
            MusicManager.PostEvent("Beurre_Death");
        }
        else
        {
            #if !UNITY_WEBGL
                AkSoundEngine.PostEvent("Beurre_Death", musicPlayer);
            #endif
        }
        
        scontinue.GameOver();
        isContinue = true;
        OnGamePaused();
    }

    public override void OnHit(int hitPoint)
    {
        if(isInvincible)
        {
            return;
        }

        screenshake.Shake(0.3f);

        if (MusicManager.WebGLBuildSupport)
        {
            MusicManager.PostEvent("Beurre_Hit");
        }
        else
        {
            #if !UNITY_WEBGL
                AkSoundEngine.PostEvent("Beurre_Hit", musicPlayer);
            #endif
        }

        particleSystem.Play();

        // Puts the player invulnerable for a short time
        StartCoroutine("InvincibleCoroutine");
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

    /**
     * The player will be invincible after getting hit
     * by a monster to avoid massive hits at the same time
     */
    private IEnumerator InvincibleCoroutine()
    {
        int damageBuffer  = damageOnCollision;
        isInvincible      = true;
        damageOnCollision = 0;
        sphereController.SetSphereDamage(0);

        yield return new WaitForSeconds(0.8f);

        isInvincible      = false;
        damageOnCollision = damageBuffer;
        sphereController.SetSphereDamage(1);
    }

    /**
     * Couroutine that make the player sprite blinking
     * when he is invincible
     */
    private IEnumerator InvincibleBlinking()
    {
        while(true)
        {
            while(isInvincible)
            {
                playerSpriteRenderer.enabled = false;
                sphereController.SetSphereVisible(false);

                yield return new WaitForSeconds(0.1f);
                playerSpriteRenderer.enabled = true;
                sphereController.SetSphereVisible(true);

                yield return new WaitForSeconds(0.1f);
            }

            yield return new WaitForEndOfFrame();
        }
    }
}
