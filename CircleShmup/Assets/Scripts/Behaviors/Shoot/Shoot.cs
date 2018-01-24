using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Shoot behavior class
 * @class Shoot
 */
public class Shoot : Behavior
{

    public int bulletCount;

    //Animator
    private Animator animator;

    //Player
    private GameObject player;


    public float ShootDelay = 1f;

    private float timer = 0;

    public Bullet[] bullets;

    public float[] bulletsInformation;

    //Todo change Animation Timer function Animation Lenght
    private float animationTimer = 0.75f;

    void Start()
    {

        bulletCount = 0;

        player = GameObject.FindGameObjectWithTag("Player");
        animator = this.GetComponent<Animator>();

        //If GD failed in configuration : TODO
        if (bullets.Length != bulletsInformation.Length)
        {
            Debug.Break();
            Debug.Log("bullets.Length != bulletsProba.Length");
        }
        animator.SetBool("ShotSoon", true);
        timer = 0;

    }

    //Shot bullet every "ShootDelay"
    void FixedUpdate()
    {

        timer += Time.fixedDeltaTime;
        if (timer > ShootDelay - animationTimer)
        {
            animator.SetBool("ShotSoon", true);
            timer = 0;
        }
    }

    public void shot()
    {
        animator.SetBool("ShotSoon", false);
        shotBehavior();
    }

    protected virtual void shotBehavior()
    {
        Bullet bullet = Instantiate(chooseBullet(), this.gameObject.transform.position, Quaternion.identity);
        bullet.instancier = this;
        bulletCount++;
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
            if (previous < proba && proba <= bulletsInformation[i])
            {
                choosenBullet = bullets[i];
                return choosenBullet;
            }
            previous = bulletsInformation[i];
        }

        return choosenBullet;
    }

    /**
     * Called when the game is paused
     */
    public virtual void OnGamePaused()
    {
        // TODO
    }

    /**
     * Called when the game resumes
     */
    public virtual void OnGameResumed()
    {
        // TODO
    }
}
