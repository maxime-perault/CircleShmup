using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * TODO
 * @class EnemyArrow1
 */
public class EnemyArrow1 : Enemy
{
    public float       delayBeforeAttack; 
    private Vector3    playerBufferedPositon;
    private Vector3    entityBufferedPosition;
    private MoveLinear moveComponent;

    private Animator animator;
    private GameObject music;
    private bool rotation = true;
    private ParticleSystem particleSystem;
    private ParticleSystem PopparticleSystem;

    private TrailRenderer trail;

    private PolygonCollider2D collider;

    private SpriteRenderer sprite;

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
        sprite = this.GetComponent<SpriteRenderer>();
        sprite.enabled = false;
        StartCoroutine("HidePop");

        collider = GetComponent<PolygonCollider2D>();
        collider.enabled = false;
        StartCoroutine("SafePop");

        music = GameObject.Find("MusicPlayer");

        if(MusicManager.WebGLBuildSupport)
        {
            MusicManager.PostEvent("Ennemy_Pop");
        }
        else
        {
            #if !UNITY_WEBGL
                AkSoundEngine.PostEvent("Ennemy_Pop", music);
            #endif
        }

        
        animator = this.GetComponent<Animator>();
        trail = GetComponent<TrailRenderer>();

        entityBufferedPosition = transform.position;
        playerBufferedPositon  = GameObject.FindGameObjectWithTag("Player").transform.position;

        moveComponent = GetComponent<MoveLinear>();
        moveComponent.startPoint = entityBufferedPosition;
        moveComponent.endPoint   = playerBufferedPositon;
        moveComponent.enabled = false;

        state = EEnemyState.WaitForMove;
        StartCoroutine(WaitForMove());

        Piment_Prep_Play();

        particleSystem = GetComponentInChildren<ParticleSystem>();
        particleSystem.GetComponent<Renderer>().sortingLayerName = "UI";

        PopparticleSystem = GetComponentsInChildren<ParticleSystem>()[1];
        PopparticleSystem.GetComponent<Renderer>().sortingLayerName = "UI";
        PopparticleSystem.GetComponent<Renderer>().material.color = new Color(0.82f, 0.18f, 0.18f);

        PopparticleSystem.Play();
    }

    /**
     * Called each update
     */
    void Update()
    {
        playerBufferedPositon = GameObject.FindGameObjectWithTag("Player").transform.position;;

        if (rotation)
        {
            Vector3 diff = playerBufferedPositon - transform.position;
            diff.Normalize();
            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);
        }


        switch (state)
        {
            case EEnemyState.EnableMoveComponent: EnableMoveComponent(); break;
            case EEnemyState.DoUntilDeath:                               break;
            default:
                break;
        }
    }

    IEnumerator SafePop()
    {
        yield return new WaitForSeconds(1f);
        enableCollision();
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
        animator.SetBool("Collision", true);
    }

    /**
     * Called when the game is paused
     */
    public override void OnGamePaused()
    {
        previousState = state;
        state         = EEnemyState.EnemyPaused;

        // Disables components
        moveComponent.enabled = false;
    }

    /**
     * Called when the game resumes
     */
    public override void OnGameResumed()
    {
        // Restores state
        state = previousState;

        moveComponent.enabled = true;
    }

    /**
     * Called when the entity is dead
     */
    public override void OnEntityDeath()
    {
        Piment_Prep_Stop();
        animator.SetBool("Collision", true);
        trail.enabled = false;
        if (handle)
        {
            handle.OnEnemyDeath(bufferIndex);
            Debug.Log("PimentHandle");
        }
        destroy();
    }

    /**
     * TODO
     */
    private void EnableMoveComponent()
    {
        moveComponent.enabled = true;
    }

    /**
     * TODO
     */
    private IEnumerator WaitForMove()
    {
        yield return new WaitForSeconds(delayBeforeAttack);
        moveComponent.endPoint = playerBufferedPositon;
        animator.SetBool("Rush", true);
        Piment_Charge();
        Piment_Prep_Stop();
        rotation = false;
        state = EEnemyState.EnableMoveComponent;
    }

    public override void OnEntityCollisionEnterWithArena()
    {
        if (handle)
        {
            handle.OnEnemyDeath(bufferIndex);
            Debug.Log("PimentHandle");
        }

        state = EEnemyState.WaitForMove;
        moveComponent.enabled = false;
        animator.SetBool("Collision", true);
        this.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        trail.enabled = false;

       
    }

    public void enableCollision()
    {
        collider.enabled = true;
    }

    public override void OnHit(int hitPoint)
    {
        particleSystem.Play();
        Piment_Cogne_Player();
        //OnEntityDeath();
    }

    public void destroy()
    {    
        Piment_Prep_Stop();
        if (handle)
        {
            handle.OnEnemyDeath(bufferIndex);
            Debug.Log("PimentHandle");
        }
        Destroy(this.gameObject);
    }

    public void Piment_Cogne()
    {
        if (MusicManager.WebGLBuildSupport)
        {
            MusicManager.PostEvent("Piment_Cogne");
        }
        else
        {
            #if !UNITY_WEBGL
                AkSoundEngine.PostEvent("Piment_Cogne", music);
            #endif
        } 
    }

    public void Piment_Prep_Play()
    {
        if (MusicManager.WebGLBuildSupport)
        {
            MusicManager.PostEvent("Piment_Prep_Play");
        }
        else
        {
            #if !UNITY_WEBGL
                AkSoundEngine.PostEvent("Piment_Prep_Play", music);
            #endif
        } 
    }

    public void Piment_Prep_Stop()
    {
        if (MusicManager.WebGLBuildSupport)
        {
            MusicManager.PostEvent("Piment_Prep_Stop");
        }
        else
        {
            #if !UNITY_WEBGL
                AkSoundEngine.PostEvent("Piment_Prep_Stop", music);
            #endif
        }
        
    }

    public void Piment_Charge()
    {
        if (MusicManager.WebGLBuildSupport)
        {
            MusicManager.PostEvent("Piment_Charge");
        }
        else
        {
            #if !UNITY_WEBGL
                AkSoundEngine.PostEvent("Piment_Charge", music);
            #endif
        }
    }

    public void Piment_Cogne_Player()
    {
        if (MusicManager.WebGLBuildSupport)
        {
            MusicManager.PostEvent("Piment_Cogne_Player");
        }
        else
        {
            #if !UNITY_WEBGL
                AkSoundEngine.PostEvent("Piment_Cogne_Player", music);
            #endif 
        }  
    }  
}
