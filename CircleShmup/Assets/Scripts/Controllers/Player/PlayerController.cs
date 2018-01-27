using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Controls the behaviour of the player
 * @class PlayerController
 */
[RequireComponent(
   typeof(Rigidbody2D),
   typeof(PolygonCollider2D),
   typeof(PlayerInputController))]
public class PlayerController : Entity
{
    public  bool                   paused;
    public  bool                   slide;
    public  Vector2                speed;
    public  PlayerSphereController sphereController;
    public SpriteRenderer          playerStateRenderer;
    public List<Sprite>            playerStateSprites = new List<Sprite>();

    private Rigidbody2D            body2D;
    private PolygonCollider2D      polygonCollider2D;
    private PlayerInputController  inputController;
    private ParticleSystem         particleSystem;

    private GameObject musicPlayer;
    private bool playerJustStartedToMove;
    private bool playerJustStoppedToMove;

    private PlayerAnimator Panimator;

    /**
     * Initializes the player controller by buffering 
     * all needed components
     */
    void Start()
    {
        body2D            = GetComponent<Rigidbody2D>();
        polygonCollider2D = GetComponent<PolygonCollider2D>();
        inputController   = GetComponent<PlayerInputController>();
        Panimator         = GetComponentInChildren<PlayerAnimator>();
        particleSystem    = GetComponentInChildren<ParticleSystem>();
        particleSystem.GetComponent<Renderer>().sortingLayerName = "UI";

        paused = false;
        playerJustStartedToMove = false;
        playerJustStoppedToMove = true;

        musicPlayer = GameObject.Find("MusicPlayer");
    }

    /**
     * Updates the player states
     */
    void Update()
    {
        if(paused)
        {
            return;
        }

        Vector2 axis = inputController.GetAxis();
        body2D.AddForce(new Vector2(speed.x * axis.x, speed.y * axis.y));


        // Animator hooks :)
        if(axis.x > 0)
        {
            Panimator.Right();
        }
        else if(axis.x < 0)
        {
            Panimator.Left();
        }
        
        if(axis.y > 0)
        {

        }
        else if(axis.y < 0)
        {

        }
        //

        if (axis.x == 0.0f && axis.y == 0.0f)
        {
            if (!slide)
            {
                body2D.velocity = new Vector2(0.0f, 0.0f);
            }

            if(!playerJustStoppedToMove)
            {
                OnPlayerJustStoppedToMove();
                playerJustStoppedToMove = true;
                playerJustStartedToMove = false;
            }
        }
        else
        {
            if(!playerJustStartedToMove)
            {
                OnPlayerJustStartedToMove();
                playerJustStartedToMove = true;
                playerJustStoppedToMove = false;
            }
        }

        if (inputController.IsAddingSphere())
        {
            sphereController.AddSphere();
        }
        else if (inputController.IsRemovingSphere())
        {
            sphereController.RemoveSphere();
        }

        bool bIncreaseRadius = false;
        if (inputController.IsClockwiseRotation())
        {
            bIncreaseRadius = true;
            sphereController.ReverseClockwise();
        }
        else if (inputController.IsCounterClockwiseRotation())
        {
            bIncreaseRadius = true;
            sphereController.ReverseCounterClockwise();
        }

        if(bIncreaseRadius)
        {
            sphereController.IncreaseRadius();
        }
        else
        {
            sphereController.DecreaseRadius();
        }

        // Player life hook
        switch (hitPoint)
        {
            case 0: playerStateRenderer.sprite = playerStateSprites[0]; break;
            case 1: playerStateRenderer.sprite = playerStateSprites[1]; break;
            case 2: playerStateRenderer.sprite = playerStateSprites[2]; break;
            case 3: playerStateRenderer.sprite = playerStateSprites[3]; break;
            case 4: playerStateRenderer.sprite = playerStateSprites[4]; break;
            case 5: playerStateRenderer.sprite = playerStateSprites[5]; break;
            case 6: playerStateRenderer.sprite = playerStateSprites[5]; break;
            default: break;
        }
    }

    /**
     * TODO
     */
    private void OnPlayerJustStartedToMove()
    {
        AkSoundEngine.PostEvent("Beurre_Move", musicPlayer);
    }

    /**
     * TODO
     */
    private void OnPlayerJustStoppedToMove()
    {
        AkSoundEngine.PostEvent("Beurre_Stop", musicPlayer);
    }

    /**
     * TODO
     */
    public override void OnEntityDeath()
    {
        AkSoundEngine.PostEvent("Beurre_Death", musicPlayer);

        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.OnGameOver();
    }

    public override void OnHit(int hitPoint)
    {
        AkSoundEngine.PostEvent("Beurre_Hit", musicPlayer);
        particleSystem.Play();
    }

    /**
     * Called when the game is paused
     */
    public void OnGamePaused()
    {
        paused = true;
        sphereController.OnGamePaused();
    }

    /**
     * Called when the game resumes
     */
    public void OnGameResumed()
    {
        paused = false;
        sphereController.OnGameResumed();
    }
}
