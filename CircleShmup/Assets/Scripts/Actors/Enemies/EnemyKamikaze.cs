using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * 
 * @class EnemyKamikaze
 */
public class EnemyKamikaze : Enemy
{
    public SeekElliptic seekBehavior = new SeekElliptic();

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
}
