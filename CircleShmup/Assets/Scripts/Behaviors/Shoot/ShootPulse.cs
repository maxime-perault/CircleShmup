using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPulse : Shoot
{

    //Pour le chooseBullet
    private int cpt = 0;
    private int actualBullet = 0;

    //Pour le shotBehavior
    private int cptPulse = 0;
    private int actualPulseBullet = 0;
    public int numberBulletPerPulse = 0;

    //The shot behavior
    protected override void shotBehavior()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        for (int i = 0; i < numberBulletPerPulse; i++)
        {
            Bullet bulletShot = Instantiate(chooseBullet(), this.gameObject.transform.position, Quaternion.identity);
            bulletShot.instancier = this;
            bulletCount++;

            Rigidbody2D body = bulletShot.gameObject.GetComponent<Rigidbody2D>();
            //Set Direction then shot
            Vector2 goalPosition = new Vector2(player.transform.position.x, player.transform.position.y);
            Vector2 direction = goalPosition - body.position;
            direction = (Vector2)(Quaternion.Euler(0, 0, 360 / numberBulletPerPulse * i) * direction);
            body.velocity = direction.normalized * bulletShot.speed;
        }

        //cptPulse++;

        //if(cptPulse == numberBulletPerPulse)
        //{
        //    cptPulse = 0;
        //    actualPulseBullet = 0;
        //}
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
