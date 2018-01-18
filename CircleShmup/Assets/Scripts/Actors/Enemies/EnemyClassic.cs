using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Base class for all enemies type "classic"
 * @class EnemyClassic
 */
public class EnemyClassic : Enemy
{
    public float shootDelay;

    /**
     * States of the base enemy
     */
    private enum EBaseState
    {
        WaitForShoot,
        EnableShootComponent,
        DoUntilDeath
    }

    private EBaseState  state;
    private Shoot       shootComponent;
    private IEnumerator waitCoroutine;

    /**
     * Called once at start
     */
    protected void BaseStart()
    {
        state = EBaseState.WaitForShoot;
        shootComponent = GetComponent<Shoot>();

        shootComponent.enabled = false;

        // Starting the coroutine
        waitCoroutine = WaitForShoot();
        StartCoroutine(waitCoroutine);
    }

    /**
     * Called on each update
     */
    protected void BaseUpdate()
    {
        switch (state)
        {
            case EBaseState.EnableShootComponent: EnableShootComponent(); break;
            case EBaseState.DoUntilDeath:                                 break;
            default:
                break;
        }
    }

    /**
     * Small coroutines to wait the delay before
     * shooting
     */
    private IEnumerator WaitForShoot()
    {
        yield return new WaitForSeconds(shootDelay);
        state = EBaseState.EnableShootComponent;
    }

    /**
     * Enables the shoot component
     */
    private void EnableShootComponent()
    {
        shootComponent.enabled = true;
    }
}
