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
public class PlayerController : MonoBehaviour
{
    public  Vector2                speed;
    public  PlayerSphereController sphereController;

    private Rigidbody2D            body2D;
    private PolygonCollider2D      polygonCollider2D;
    private PlayerInputController  inputController;
    

    /**
     * Initializes the player controller by buffering 
     * all needed components
     */
    void Start()
    {
        body2D            = GetComponent<Rigidbody2D>();
        polygonCollider2D = GetComponent<PolygonCollider2D>();
        inputController   = GetComponent<PlayerInputController>();
    }

    /**
     * Updates the player states
     */
    void Update()
    {
        Vector2 axis = inputController.GetAxis();
        body2D.AddForce(new Vector2(speed.x * axis.x, speed.y * axis.y));

        if (inputController.IsAddingSphere())
        {
            sphereController.AddSphere();
        }
        else if (inputController.IsRemovingSphere())
        {
            sphereController.RemoveSphere();
        }

        if (inputController.IsIncreasingRadius())
        {
            sphereController.IncreaseRadius();
        }
        else
        {
            sphereController.DecreaseRadius();
        }

        if (inputController.IsReversingRotation())
        {
            sphereController.ReverseRotation();
        }
    }
}
