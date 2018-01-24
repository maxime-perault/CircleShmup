using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootStrait : Shoot
{

    private int cpt = 0;
    private int actualBullet = 0;

    //The shot behavior
    protected override void shotBehavior()
    {
        Bullet bulletShot = Instantiate(chooseBullet(), this.gameObject.transform.position, Quaternion.identity);
        bulletShot.instancier = this;
        bulletCount++;


        Rigidbody2D body = bulletShot.gameObject.GetComponent<Rigidbody2D>();

        //Set Direction then shot
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector2 goalPosition = new Vector2(player.transform.position.x, player.transform.position.y);
        Vector2 direction = goalPosition - body.position;
        body.velocity = direction.normalized * bulletShot.speed;

    }

    //Choose the bullet shot
    // !! Deterministic version !!
    protected override Bullet chooseBullet()
    {
        Bullet choosenBullet = bullets[0];

        if (bullets.Length == 1)
        {
            return choosenBullet;
        }

        if (cpt < bulletsInformation[actualBullet])
        {
            cpt += 1;
        }
        else if (actualBullet + 1 < bulletsInformation.Length)
        {
            actualBullet++;
            cpt = 1;
            choosenBullet = bullets[actualBullet];
        }
        else
        {
            actualBullet = 0;
            cpt = 1;
            choosenBullet = bullets[actualBullet];
        }


        return choosenBullet;
    }


}
