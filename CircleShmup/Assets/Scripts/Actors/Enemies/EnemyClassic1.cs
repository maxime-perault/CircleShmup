using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Static enemy 
 * @class EnemyClassic1
 */
public class EnemyClassic1 : EnemyClassic
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
        if (handle)
        {
            handle.OnEnemyDeath();
        }

        Destroy(this.gameObject);
    }
}
