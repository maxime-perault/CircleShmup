using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Enemy who moves in a elliptic way
 * @class EnemyClassic2
 */
public class EnemyClassic2 : EnemyClassic
{
    /**
     * Called once at start
     */
    void Start()
    {
        BaseStart();
    }

    /**
     * Called on each update
     */
    void Update()
    {
        BaseUpdate();
    }

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
        // Notifies that this enemy is dead
        if(handle)
        {
            handle.OnEnemyDeath();
        }

        Destroy(this.gameObject);
    }
}
