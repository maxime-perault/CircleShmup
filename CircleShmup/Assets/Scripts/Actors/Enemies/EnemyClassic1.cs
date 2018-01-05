using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * TODO
 * @class EnemyClassic1
 */
public class EnemyClassic1 : Enemy
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
        Destroy(this.gameObject);
    }
}
