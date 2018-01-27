using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Enemy who moves in a elliptic way
 * @class EnemyClassic2
 */
public class EnemyClassic2 : EnemyClassic
{
    private Animator     animator;
    private GameObject   music;

    private MoveElliptic moveComponent;

    private int initialHitPoint;

    private ParticleSystem particleSystem;
    private ParticleSystem PopparticleSystem;

    private TrailRenderer trail;

    private PolygonCollider2D collider;

    private SpriteRenderer sprite;

    /**
     * Called once at start
     */
    void Start()
    {

        sprite = this.GetComponent<SpriteRenderer>();
        sprite.enabled = false;
        StartCoroutine("HidePop");

        collider = GetComponent<PolygonCollider2D>();
        collider.enabled = false;
        StartCoroutine("SafePop");

        music = GameObject.Find("MusicPlayer");
        BaseStart();
        AkSoundEngine.PostEvent("Ennemy_Pop", music);
        animator = this.GetComponent<Animator>();
        animator.SetBool("LowLife", false);
        trail = GetComponent<TrailRenderer>();

        moveComponent  = GetComponent<MoveElliptic>();

        initialHitPoint = hitPoint;

        particleSystem = GetComponentInChildren<ParticleSystem>();
        particleSystem.GetComponent<Renderer>().sortingLayerName = "UI";

        PopparticleSystem = GetComponentsInChildren<ParticleSystem>()[1];
        PopparticleSystem.GetComponent<Renderer>().sortingLayerName = "UI";
        PopparticleSystem.GetComponent<Renderer>().material.color = new Color(0.82f, 0.18f, 0.18f);

        PopparticleSystem.Play();
    }

    /**
     * Called on each update
     */
    void Update()
    {
        BaseUpdate();
    }

    IEnumerator HidePop()
    {
        yield return new WaitForSeconds(0.15f);
        sprite.enabled = true;
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
        moveComponent.enabled = false;
        shootComponent.OnGamePaused();
    }

    /**
     * Called when the game resumes
     */
    public override void OnGameResumed()
    {
        moveComponent.enabled = true;
        shootComponent.OnGameResumed();
    }

    /**
     * Called when the entity is dead
     */
    public override void OnEntityDeath()
    {
        Mais_Death();

        // Notifies that this enemy is dead
        if (handle)
        {
            handle.OnEnemyDeath(bufferIndex);
        }

        this.shootComponent.alive = false;

        trail.enabled = false;

        animator.SetBool("Die", true);

    }

    public void destroy()
    {
        Destroy(this.gameObject);
    }

    public override void OnHit(int hitPoint)
    {
        particleSystem.Play();
        if ( hitPoint <= initialHitPoint/2)
            animator.SetBool("LowLife", true);
        Mais_Hit();
    }

    public void enableCollision()
    {
        collider.enabled = true;
    }

    IEnumerator SafePop()
    {
        yield return new WaitForSeconds(1f);
        enableCollision();
    }

    public void Mais_Move_Up()
    {
        AkSoundEngine.PostEvent("Mais_Move_Up", music);
    }

    public void Mais_Move_Down()
    {
        AkSoundEngine.PostEvent("Mais_Move_Down", music);
    }

    public void Mais_Hit()
    {
        AkSoundEngine.PostEvent("Mais_Hit", music);
    }

    public void Mais_Death()
    {
        AkSoundEngine.PostEvent("Mais_Death", music);
    }

    public void Mais_Shot_Prep()
    {
        AkSoundEngine.PostEvent("Mais_Shot_Prep", music);
    }

    public void Mais_Shot_Fire()
    {
        AkSoundEngine.PostEvent("Mais_Shot_Fire", music);
    }
}
