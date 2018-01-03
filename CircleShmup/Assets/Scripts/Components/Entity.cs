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
    public int  hitPoint;

    /**
     * TODO
     */
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(this.gameObject.tag == "Player")
        {

        }
        else if(this.gameObject.tag == "Enemy")
        {

        }
    }
}
