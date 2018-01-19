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
    private Rigidbody2D body;
    private Vector2 goalPosition;

    public float speed = 1f;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        goalPosition = new Vector2(player.transform.position.x, player.transform.position.y);
        body = this.gameObject.GetComponent<Rigidbody2D>();

        //Set Direction then shot
        Vector2 direction = goalPosition - body.position;
        body.velocity = direction.normalized * speed;
    }


    public override void OnEntityCollisionEnterWithArena()
    {
        Destroy(this.gameObject,5); //Change time
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
