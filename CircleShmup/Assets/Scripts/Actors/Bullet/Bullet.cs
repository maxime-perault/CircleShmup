using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Bullet behavior class
 * @class Bullet
 */
public class Bullet : Entity
{
    private GameObject player;
    private Vector2    goalPosition;
    public Shoot       instancier;

    public float speed = 1f;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }


    public override void OnEntityCollisionEnterWithArena()
    {
        Destroy(this.gameObject, 0.1f); //Change time
    }

    /**
     * TODO
     */
    public override void OnEntityCollisionEnterWithPlayer()
    {
        player.GetComponent<PlayerController>().OnDamage(this.damageOnCollision);
        OnEntityDeath();
    }

    /**
     * TODO
     */
    public override void OnEntityDeath()
    {
        Destroy(this.gameObject);
    }
}
