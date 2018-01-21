using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Shoot behavior class
 * @class Shoot
 */
public class Shoot : Behavior {


    //Animator
    public Animator animator;

    //Player
    private GameObject player;


    public float ShootDelay = 1f;

    private float timer = 0;

    public Bullet[] bullets;

    public float[] bulletsInformation;


    private float animationTimer = 0.75f;

    enum State
    {
        idle = 0,
        preShot = 1,
        Shot = 2
    }

    private State state = State.idle;

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
        if (timer > ShootDelay-animationTimer)
        {
            this.state = State.preShot;
            animator.SetBool("ShotSoon", true);
            timer = 0;
        }

        if (this.state == State.preShot && animator.GetCurrentAnimatorStateInfo(0).IsName("Mais_Shot"))
        {
            animator.SetBool("ShotSoon", false);
            this.state = State.Shot;
            shot();
        }
        if(this.state == State.Shot && animator.GetCurrentAnimatorStateInfo(0).IsName("Mais_MoveNeutral"))
        {
            this.state = State.idle;
        }

    }

    private void shot()
    {
            shotBehavior();        
    }

    protected virtual void shotBehavior()
    {
        Instantiate(chooseBullet(), this.gameObject.transform.position, Quaternion.identity);
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
