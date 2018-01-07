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

    public float[] bulletsProba;


    private GameObject player;

    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");

        //If GD failed in configuration : TODO
        if (bullets.Length != bulletsProba.Length)
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
            Instantiate(chooseBullet(), this.gameObject.transform.position, Quaternion.identity);
            timer = 0;
        }

    }


    //Choose the bullet shot
    private Bullet chooseBullet()
    {
        Bullet choosenBullet = bullets[0];
        float proba = Random.Range(0f, 1f);
        float previous = 0f;

        if (bullets.Length == 1)
        {
            return choosenBullet;
        }

        for (int i = 0; i < bulletsProba.Length; i++)
        {
            if( previous < proba && proba <= bulletsProba[i])
            {
                choosenBullet = bullets[i];
                return choosenBullet;
            }
            previous = bulletsProba[i];
        }

        return choosenBullet;
    }
}
