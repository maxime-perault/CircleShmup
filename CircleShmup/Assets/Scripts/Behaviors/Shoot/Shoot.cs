using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Shoot behavior class
 * @class Shoot
 */
public class Shoot : Behavior {

   

    public float ShootDelay = 1f;

    private float timer = 0;

    public Bullet[] bullets;

    public float[] bulletsInformation;


    private GameObject player;

    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");

        //If GD failed in configuration : TODO
        if (bullets.Length != bulletsInformation.Length)
        {
            Debug.Break();
            Debug.Log("bullets.Length != bulletsProba.Length");
        }

	}
	
    //Shot bullet every "ShootDelay"
	void FixedUpdate () {

        timer += Time.fixedDeltaTime;
        if (timer > ShootDelay)
        {
            shot();
            timer = 0;
        }

    }


    protected virtual Bullet shot()
    {
        return Instantiate(chooseBullet(), this.gameObject.transform.position, Quaternion.identity); ;
    }


    //Choose the bullet shot
    // !! Probabilistic version !! 
    protected virtual Bullet chooseBullet()
    {
        Bullet choosenBullet = bullets[0];
        float proba = Random.Range(0f, 1f);
        float previous = 0f;

        if (bullets.Length == 1)
        {
            return choosenBullet;
        }

        for (int i = 0; i < bulletsInformation.Length; i++)
        {
            if( previous < proba && proba <= bulletsInformation[i])
            {
                choosenBullet = bullets[i];
                return choosenBullet;
            }
            previous = bulletsInformation[i];
        }

        return choosenBullet;
    }
}
