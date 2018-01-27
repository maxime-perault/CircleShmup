using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * The base enemy class
 * @class ZucchiniTomate
 */
public class ZucchiniTomate : Enemy
{

    private ParticleSystem particleSystem;

    private CircleCollider2D collider;
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

    private void Start()
    {
        particleSystem = GetComponentInChildren<ParticleSystem>();
        particleSystem.GetComponent<Renderer>().sortingLayerName = "UI";

        collider = GetComponent<CircleCollider2D>();
        collider.enabled = false;
        StartCoroutine("SafePop");
    }

    IEnumerator SafePop()
    {
        yield return new WaitForSeconds(1f);
        enableCollision();
    }

    public void enableCollision()
    {
        collider.enabled = true;
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
        particleSystem.Play();
        Debug.Log("Tomate : OnEntityDeath");
        enemyInstance.OnTomateDestroyed(side);
    }
}
