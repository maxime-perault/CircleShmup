using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * The base enemy class
 * @class ZucchiniTomate
 */
public class ZucchiniTomate : Enemy
{
    /**
     * TODO
     */
    public enum ETomateSide
    {
        North,
        East,
        West,
        South
    }

    public ETomateSide    side;
    public EnemyZucchini1 enemyInstance;

    /**
     * Called when the entity collides with the player
     */
    public override void OnEntityCollisionEnterWithPlayer()
    {
        // None
    }

    /**
     * Called when the entity is dead
     */
    public override void OnEntityDeath()
    {
        Debug.Log("Tomate : OnEntityDeath");
        enemyInstance.OnTomateDestroyed(side);
    }
}
