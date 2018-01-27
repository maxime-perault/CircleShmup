using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Main behavior controller for the zucchini enemy
 * @class EnemyZucchini1
 */
public class EnemyZucchini1 : Enemy
{
    public float rotationSpeed;
    public float rotationDirection;
    public float delayBeforeAttack;

    public Transform[] TabTransf;

    public Sprite[] SpriteList;

    public Transform spriteGroup;

    private SpriteRenderer[] SpriteToHide;

    private int tomateCounter;
    private MoveElliptic moveComponent;

    private GameObject music;
    private PlayerSphereController sphereController;

    private Animator animator;

    private ParticleSystem PopparticleSystem;

    private TrailRenderer trail;

    private CircleCollider2D collider;

    /**
     * States of the arrow enemy
     */
    private enum EEnemyState
    {
        WaitForMove,
        EnemyPaused,
        EnableMoveComponent,
        DoUntilDeath
    }

    private EEnemyState state;
    private EEnemyState previousState;

    /**
     * Called at start
     */
    void Start()
    {
        SpriteToHide = GetComponentsInChildren<SpriteRenderer>();
        GetComponent<SpriteRenderer>().enabled = false;
        foreach (SpriteRenderer sprite in SpriteToHide)
        {
            sprite.enabled = false;
        }
        StartCoroutine("HidePop");

        collider = GetComponent<CircleCollider2D>();
        collider.enabled = false;
        StartCoroutine("SafePop");


        music             = GameObject.Find("MusicPlayer");
        GameObject player = GameObject.FindWithTag("Player");
        sphereController  = player.transform.GetComponentInChildren<PlayerSphereController>();
        animator = GetComponent<Animator>();
        trail = GetComponent<TrailRenderer>();

        AkSoundEngine.PostEvent("Ennemy_Pop", music);

        tomateCounter = 0;
        moveComponent = GetComponent<MoveElliptic>();

        PopparticleSystem = GetComponentsInChildren<ParticleSystem>()[0];
        PopparticleSystem.GetComponent<Renderer>().sortingLayerName = "UI";
        //PopparticleSystem.GetComponent<Renderer>(). .material.color = new Color(0.82f, 0.18f, 0.18f);

        if (moveComponent != null)
        {
            moveComponent.enabled = false;
            state = EEnemyState.WaitForMove;
            StartCoroutine(WaitForMove());
        }
        else
        {
            state = EEnemyState.DoUntilDeath;
        }
    }

    /**
     * Called each update
     */
    void Update()
    {
        switch (state)
        {
            case EEnemyState.EnableMoveComponent: EnableMoveComponent(); break;
            case EEnemyState.DoUntilDeath: DoUntilDeath(); break;
            default:
                break;
        }
    }

    IEnumerator HidePop()
    {
        yield return new WaitForSeconds(0.15f);
        GetComponent<SpriteRenderer>().enabled = true;
        foreach (SpriteRenderer sprite in SpriteToHide)
        {
            sprite.enabled = true;
        }
    }

    IEnumerator SafePop()
    {
        yield return new WaitForSeconds(1f);
        enableCollision();     
    }

    /**
     * TODO
     */
    public void DoUntilDeath()
    {
        spriteGroup.Rotate(new Vector3(0, 0, rotationSpeed) * Time.deltaTime * rotationDirection);
    }

    /**
     * Called when the entity collides with the player
     */
    public override void OnEntityCollisionEnterWithPlayer()
    {
        // None
    }

    /**
     * Called when the game is paused
     */
    public override void OnGamePaused()
    {
        // None
    }

    /**
     * Called when the game resumes
     */
    public override void OnGameResumed()
    {
        // None
    }

    /**
     * Called  when a tomate is destroyed
     */
    public void OnTomateDestroyed(ZucchiniTomate.ETomateSide side)
    {
        Brochette_Tomato_Destroy();
        switch (side)
        {
            case ZucchiniTomate.ETomateSide.North:
                Debug.Log("North down !");
                TabTransf[4].GetComponent<SpriteRenderer>().sprite = SpriteList[5];
                break;
            case ZucchiniTomate.ETomateSide.South:
                TabTransf[1].GetComponent<SpriteRenderer>().sprite = SpriteList[2];
                Debug.Log("South down !");
                break;
            case ZucchiniTomate.ETomateSide.West:
                TabTransf[2].GetComponent<SpriteRenderer>().sprite = SpriteList[3];
                Debug.Log("West  down !");  
                break;
            case ZucchiniTomate.ETomateSide.East:
                TabTransf[3].GetComponent<SpriteRenderer>().sprite = SpriteList[4];
                Debug.Log("East  down !");
                break;
            default: break;
        }

        tomateCounter++;

        if (tomateCounter == 2)
        {
            TabTransf[0].GetComponent<SpriteRenderer>().sprite = SpriteList[0];
        }

        if (tomateCounter == 4)
        {
            TabTransf[0].GetComponent<SpriteRenderer>().sprite = SpriteList[1];
            OnAllTomateDestroyed();
        }
    }

    /**
     * Called when all tomates have been destroyed
     */
    public void OnAllTomateDestroyed()
    {
        isInvincible = false;
        Debug.Log("Invincibility removed !");
    }

    /**
     * Called when the entity is dead
     */
    public override void OnEntityDeath()
    {
        Brochette_Death();
        // Notifies that this enemy is dead
        if (handle)
        {
            handle.OnEnemyDeath(bufferIndex);
        }
        TabTransf[0].GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
        TabTransf[1].GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
        TabTransf[2].GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
        TabTransf[3].GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
        TabTransf[4].GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
        TabTransf[5].GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
        trail.enabled = false;

        //Animator
        animator.SetBool("Die", true);
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }

    public override void OnHit(int hitPoint)
    {
        base.OnHit(hitPoint);
    }


    /**
     * TODO
     */
    public override void OnInvincibleHit(int hitPoint)
    {
        // Reverse player rotation direction
        sphereController.Reverse();
        AkSoundEngine.PostEvent("Brochette_Hit_Invincible", music);
    }

    /**
     * TODO
     */
    private void EnableMoveComponent()
    {
        moveComponent.enabled = true;
        state = EEnemyState.DoUntilDeath;
    }


    public void enableCollision()
    {
        collider.enabled = true;
    }

    /**
     * TODO
     */
    private IEnumerator WaitForMove()
    {
        yield return new WaitForSeconds(delayBeforeAttack);
        state = EEnemyState.EnableMoveComponent;
    }

    public void Brochette_Tomato_Destroy()
    {
        AkSoundEngine.PostEvent("Brochette_Tomato_Destroy", music);
    }

    public void Brochette_Death()
    {
        AkSoundEngine.PostEvent("Brochette_Death", music);
    }

    public void Brochette_Hit_Invincible()
    {
        AkSoundEngine.PostEvent("Brochette_Hit_Invincible", music);
    }
}
