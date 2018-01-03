using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Kamikaze enemy - Derived class from Enemy
 * @class EnemyKamikaze
 */
public class EnemyKamikaze : Enemy
{
    public SeekElliptic seekBehavior = new SeekElliptic();

    /**
     * TODO
     */
    public void Start()
    {
        seekBehavior.Init();
    }

    /**
     * TODO
     */
    public void Update()
    {
        seekBehavior.Update(Time.deltaTime);
    }

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
        Debug.Log("Kamikaze : OnEntityDeath");
        Destroy(this.gameObject);
    }
}
