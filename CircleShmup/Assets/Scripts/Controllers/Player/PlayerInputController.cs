﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Manages inputs for the Player controller
 * @class PlayerInputController
 */
public class PlayerInputController : MonoBehaviour
{
    public bool canAddSphere                = true;
    public bool canRemoveSphere             = true;
    public bool canIncreaseRadius           = true;
    public bool canClockwiseRotation        = true;
    public bool canCounterClockwiseRotation = true;

    [SerializeField] public string verticalInput                 = "Vertical";
    [SerializeField] public string horizontalInput               = "Horizontal";
                                                                 
    [SerializeField] public string addSphereInput                = "AddSphere";
    [SerializeField] public string removeSphereInput             = "RemoveSphere";
    [SerializeField] public string increaseRadiusInput           = "IncreaseRadius";
    [SerializeField] public string clockwiseRotationInput        = "ClockwiseRotation";
    [SerializeField] public string counterClockwiseRotationInput = "CounterClockwiseRotation";

    private bool        mouseLeftHold;
    private bool        mouseRightHold;
    private GameManager gameManagerInstance;

    /**
     * Called at behavior start
     */
    public void Start()
    {
        // Buffering the game manager
        mouseLeftHold       = false;
        mouseRightHold      = false;
        gameManagerInstance = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (!gameManagerInstance)
        {
            Debug.LogError("Cannot find the game manager ...");
        }
    }

    /**
     * Returns the horizontal and vertical axis
     * @return A vector2 containing the axis
     */
    public Vector2 GetAxis()
    {
        Vector2 axis = new Vector2(
            Input.GetAxisRaw(horizontalInput), 
            Input.GetAxisRaw(verticalInput));

        if (axis == Vector2.zero)
        {
            axis.x =
                (gameManagerInstance.GetKey(GameManager.e_input.RIGHT) ?  1.0f : 0.0f) +
                (gameManagerInstance.GetKey(GameManager.e_input.LEFT)  ? -1.0f : 0.0f);

            axis.y =
                (gameManagerInstance.GetKey(GameManager.e_input.UP)   ?  1.0f : 0.0f) +
                (gameManagerInstance.GetKey(GameManager.e_input.DOWN) ? -1.0f : 0.0f);
        }

        axis.y *= gameManagerInstance.invertYaxis;

        return axis;
    }
    
    /**
     * Tells if the player is pressing the button "AddSphere"
     * @return True or false
     */
    public bool IsAddingSphere()
    {
        return (canAddSphere) ? Input.GetButton(addSphereInput) : false;
    }

    /**
     * Tells if the player is pressing the button "RemoveSphere"
     * @return True or false
     */
    public bool IsRemovingSphere()
    {
        return (canRemoveSphere) ? Input.GetButton(removeSphereInput) : false;
    }

    /**
     * Tells if the player is pressing the button "IncreaseRadius"
     * @return True or false
     */
    public bool IsIncreasingRadius()
    {
        // Deprecated
        // return (canIncreaseRadius) ? 
        //     (Input.GetButton(increaseRadiusInput)) ||
        //     (gameManagerInstance.GetKeyDown(GameManager.e_input.ACCEPT)) : false;

        return false;
    }

    /**
     * Tells if the player is pressing the button "ClockwiseRotation"
     * @return True or false
     */
    public bool IsClockwiseRotation()
    {
        if(gameManagerInstance.GetKeyDown(GameManager.e_input.TURNLEFT))
        {
            mouseLeftHold = true;
        }
        else if(gameManagerInstance.GetKeyUp(GameManager.e_input.TURNLEFT))
        {
            mouseLeftHold = false;
        }

        if (Input.GetAxis(clockwiseRotationInput) == 1.0f || mouseLeftHold == true)
            return true;
        return false;
    }

    /**
    * Tells if the player is pressing the button "CounterClockwiseRotation"
    * @return True or false
    */
    public bool IsCounterClockwiseRotation()
    {
        if (gameManagerInstance.GetKeyDown(GameManager.e_input.TURNRIGHT))
        {
            mouseRightHold = true;
        }
        else if (gameManagerInstance.GetKeyUp(GameManager.e_input.TURNRIGHT))
        {
            mouseRightHold = false;
        }

        if (Input.GetAxis(counterClockwiseRotationInput) == 1.0f || mouseRightHold == true)
            return true;
        return false;
    }
}
