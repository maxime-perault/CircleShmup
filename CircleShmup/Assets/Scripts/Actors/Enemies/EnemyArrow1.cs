using UnityEngine;
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
        switch(state)
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
        // None
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
        // Notifies that this enemy is dead
        if (handle)
        {
            handle.OnEnemyDeath(bufferIndex);
        }

        Destroy(this.gameObject);
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
        state = EEnemyState.EnableMoveComponent;
    }
}
