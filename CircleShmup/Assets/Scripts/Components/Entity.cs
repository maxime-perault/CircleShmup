using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Entity component for all living game objects
 * @class Entity
 */
public class Entity : MonoBehaviour
{
    public bool isDead;
    public bool isInvincible;

    public int hitPoint;
    public int damageOnCollision;

    public virtual void OnEntityCollisionEnterWithPlayer()
    { /* None */ }

    public virtual void OnEntityCollisionEnterWithEnemy()
    { /* None */ }

    public virtual void OnEntityCollisionEnterWithArena()
    { /* None */ }

    public virtual void OnEntityDeath()
    { /* None */ }

    /**
     * Check the collision stateOnTriggerEnter2D
     * @param collision The collision point
     */
    public void OnTriggerEnter2D(Collider2D collision)
    {
        bool isEntity = false;
        if (this.gameObject.tag == "Player")
        {
            if (collision.gameObject.tag == "Enemy")
            {
                isEntity = true;
                OnEntityCollisionEnterWithEnemy();
            }
        }
        else if (this.gameObject.tag == "Enemy")
        {
            if (collision.gameObject.tag == "Player")
            {
                isEntity = true;
                OnEntityCollisionEnterWithPlayer();
            }
        }
        else if (this.gameObject.tag == "Bullet")
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

    /**
     * Inflicts damages
     * @param damages The damages received
     */
    public void OnDamage(int damages)
    {
        if (isInvincible)
        {
            return;
        }

        OnHit();
        hitPoint -= damages;

        if (hitPoint <= 0)
        {
            hitPoint = 0;
            isDead = true;
            OnEntityDeath();
        }
    }

    public virtual void OnHit()
    {
        //PlaySound
    }
}
