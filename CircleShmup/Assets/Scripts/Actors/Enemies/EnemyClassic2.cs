using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Destroy(this.gameObject);
    }
}
