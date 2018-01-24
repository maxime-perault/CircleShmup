using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Main behavior controller for the zucchini enemy
 * @class EnemyZucchini1
 */
public class EnemyZucchini1 : Enemy
{
    public  float        rotationSpeed;
    public  float        rotationDirection;
    public  float        delayBeforeAttack;

    public  Transform    spriteGroup;

    private int          tomateCounter;
    private MoveElliptic moveComponent;

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
        tomateCounter = 0;
        moveComponent = GetComponent<MoveElliptic>();

        if(moveComponent != null)
        {
            moveComponent.enabled = false;
            state = EEnemyState.WaitForMove;
            StartCoroutine(WaitForMove());
        }
        else
        {
            state = EEnemyState.DoUntilDeath;
        }
    }

    /**
     * Called each update
     */
    void Update()
    {
        switch (state)
        {
            case EEnemyState.EnableMoveComponent: EnableMoveComponent(); break;
            case EEnemyState.DoUntilDeath:        DoUntilDeath();        break;
            default:
                break;
        }
    }

    /**
     * TODO
     */
    public void DoUntilDeath()
    {
        spriteGroup.Rotate(new Vector3(0, 0, rotationSpeed) * Time.deltaTime * rotationDirection);
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
       // None
    }

    /**
     * Called when the game resumes
     */
    public override void OnGameResumed()
    {
        // None
    }

    /**
     * Called  when a tomate is destroyed
     */
    public void OnTomateDestroyed(ZucchiniTomate.ETomateSide side)
    {
        switch(side)
        {
            case ZucchiniTomate.ETomateSide.North: Debug.Log("North down !"); break;
            case ZucchiniTomate.ETomateSide.South: Debug.Log("South down !"); break;
            case ZucchiniTomate.ETomateSide.West:  Debug.Log("West  down !"); break;
            case ZucchiniTomate.ETomateSide.East:  Debug.Log("East  down !"); break;
            default: break;
        }

        tomateCounter++;
        if(tomateCounter == 4)
        {
            OnAllTomateDestroyed();
        }
    }

    /**
     * Called when all tomates have been destroyed
     */
    public void OnAllTomateDestroyed()
    {
        isInvincible = false;
        Debug.Log("Invincibility removed !");
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
        state = EEnemyState.DoUntilDeath;
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
