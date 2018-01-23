using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Enemy who moves in a elliptic way
 * @class EnemyClassic2
 */
public class EnemyClassic2 : EnemyClassic
{
    private Animator animator;

    private GameObject music;

    /**
     * Called once at start
     */
    void Start()
    {
        music = GameObject.Find("MusicPlayer");
        BaseStart();
        AkSoundEngine.PostEvent("Ennemy_Pop", music);
        animator = this.GetComponent<Animator>();
        animator.SetBool("LowLife", false);
    }

    /**
     * Called on each update
     */
    void Update()
    {
        BaseUpdate();
    }

    /**
     * Called when the entity collides with the player
     */
    public override void OnEntityCollisionEnterWithPlayer()
    {
        // None
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
            handle.OnEnemyDeath();
        }
        Destroy(this.gameObject);
    }

    public override void OnHit()
    {
        animator.SetBool("LowLife", true);
        Mais_Hit();
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
