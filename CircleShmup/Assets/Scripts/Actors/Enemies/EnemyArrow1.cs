﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * TODO
 * @class EnemyArrow1
 */
public class EnemyArrow1 : Enemy
{
    public float       delayBeforeAttack; 
    private Vector3    playerBufferedPositon;
    private Vector3    entityBufferedPosition;
    private MoveLinear moveComponent;

    private Animator animator;
    private GameObject music;
    private bool rotation = true;

    /**
     * States of the arrow enemy
     */
    private enum EEnemyState
    {
        WaitForMove,
        EnemyPaused,
        EnableMoveComponent,
        DoUntilDeath
    }

    private EEnemyState state;
    private EEnemyState previousState;

    /**
     * Called at start
     */
    void Start()
    {
        music = GameObject.Find("MusicPlayer");
        AkSoundEngine.PostEvent("Ennemy_Pop", music);
        animator = this.GetComponent<Animator>();

        entityBufferedPosition = transform.position;
        playerBufferedPositon  = GameObject.FindGameObjectWithTag("Player").transform.position;

        moveComponent = GetComponent<MoveLinear>();
        moveComponent.startPoint = entityBufferedPosition;
        moveComponent.endPoint   = playerBufferedPositon;
        moveComponent.enabled = false;

        state = EEnemyState.WaitForMove;
        StartCoroutine(WaitForMove());   
    }

    /**
     * Called each update
     */
    void Update()
    {
        playerBufferedPositon = GameObject.FindGameObjectWithTag("Player").transform.position;;

        if (rotation)
        {
            Vector3 diff = playerBufferedPositon - transform.position;
            diff.Normalize();
            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);
        }


        switch (state)
        {
            case EEnemyState.EnableMoveComponent: EnableMoveComponent(); break;
            case EEnemyState.DoUntilDeath:                               break;
            default:
                break;
        }
    }

    /**
     * Called when the entity collides with the player
     */
    public override void OnEntityCollisionEnterWithPlayer()
    {
        animator.SetBool("Collision", true);
    }

    /**
     * Called when the game is paused
     */
    public override void OnGamePaused()
    {
        previousState = state;
        state         = EEnemyState.EnemyPaused;

        // Disables components
        moveComponent.enabled = false;
    }

    /**
     * Called when the game resumes
     */
    public override void OnGameResumed()
    {
        // Restores state
        state = previousState;

        moveComponent.enabled = true;
    }

    /**
     * Called when the entity is dead
     */
    public override void OnEntityDeath()
    {

        animator.SetBool("Collision", true);
        // Notifies that this enemy is dead
        if (handle)
        {
            handle.OnEnemyDeath(bufferIndex);
        }
        destroy();
    }

    /**
     * TODO
     */
    private void EnableMoveComponent()
    {
        moveComponent.enabled = true;
    }

    /**
     * TODO
     */
    private IEnumerator WaitForMove()
    {
        yield return new WaitForSeconds(delayBeforeAttack);
        moveComponent.endPoint = playerBufferedPositon;
        animator.SetBool("Rush", true);
        rotation = false;
        state = EEnemyState.EnableMoveComponent;
    }

    public override void OnEntityCollisionEnterWithArena()
    {
        state = EEnemyState.WaitForMove;
        moveComponent.enabled = false;
        animator.SetBool("Collision", true);
    }

    public override void OnHit(int hitPoint)
    {
        OnEntityDeath();
    }

    public void destroy()
    {
        Destroy(this.gameObject);
    }
}
