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
    public  bool                   slide;
    public  Vector2                speed;
    public  PlayerSphereController sphereController;

    private Rigidbody2D            body2D;
    private PolygonCollider2D      polygonCollider2D;
    private PlayerInputController  inputController;

    private bool playerJustStartedToMove;
    private bool playerJustStoppedToMove;

    /**
     * Initializes the player controller by buffering 
     * all needed components
     */
    void Start()
    {
        body2D            = GetComponent<Rigidbody2D>();
        polygonCollider2D = GetComponent<PolygonCollider2D>();
        inputController   = GetComponent<PlayerInputController>();

        playerJustStartedToMove = false;
        playerJustStoppedToMove = true;
    }

    /**
     * Updates the player states
     */
    void Update()
    {
        Vector2 axis = inputController.GetAxis();
        body2D.AddForce(new Vector2(speed.x * axis.x, speed.y * axis.y));

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
    }

    /**
     * TODO
     */
    private void OnPlayerJustStartedToMove()
    {
        Debug.Log("OnPlayerJustStartedToMove");
    }

    /**
    * TODO
    */
    private void OnPlayerJustStoppedToMove()
    {
        Debug.Log("OnPlayerJustStoppedToMove");
    }
}
