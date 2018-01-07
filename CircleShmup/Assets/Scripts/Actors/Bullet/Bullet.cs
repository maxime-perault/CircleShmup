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


    /**
     * Check the collision state
     * @param collision The collision point
     */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool isEntity = false;
        if (this.gameObject.tag == "Bullet")
        {
            if (collision.gameObject.tag == "Player")
            {
                isEntity = true;
                OnEntityCollisionEnterWithPlayer();
            }
            if (collision.gameObject.tag == "Arena")
            {
                isEntity = false;
                OnEntityCollisionEnterWithArena();
            }
        }
        if (isEntity && !isDead)
        {
            // Gets the entity components
            Entity entity = collision.gameObject.GetComponent<Entity>();
            OnDamage(entity.damageOnCollision);
        }
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
