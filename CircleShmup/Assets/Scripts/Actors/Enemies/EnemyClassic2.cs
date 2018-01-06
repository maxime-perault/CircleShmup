using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * TODO
 * @class EnemyClassic2
 */
public class EnemyClassic2 : Enemy
{
    /**
     * TODO
     */
    public override void OnEntityCollisionEnterWithPlayer()
    {
        // None
    }

    /**
     * TODO
     */
    public override void OnEntityDeath()
    {
        if(handle)
        {
            handle.OnEnemyDeath();
        }

        Destroy(this.gameObject);
    }
}
