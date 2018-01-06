using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Watches collisions with player sphere
 * @class Sphere
 */
public class Sphere : MonoBehaviour
{
    public int damages;

    /**
     * A collision occured
     * @param collision The collision ...
     */
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.OnDamage(damages);
        }
    }
}
