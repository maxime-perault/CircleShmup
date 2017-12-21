using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Manages inputs for the Player controller
 * @class PlayerInputController
 */
public class PlayerInputController : MonoBehaviour
{
    [SerializeField] private string verticalInput        = "Vertical";
    [SerializeField] private string horizontalInput      = "Horizontal";
   
    [SerializeField] private string addSphereInput       = "AddSphere";
    [SerializeField] private string removeSphereInput    = "RemoveSphere";
    [SerializeField] private string increaseRadiusInput  = "IncreaseRadius";
    [SerializeField] private string reverseRotationInput = "ReverseRotation";
    
    /**
     * Returns the horizontal and vertical axis
     * @return A vector2 containing the axis
     */
    public Vector2 GetAxis()
    {
        return new Vector2(
            Input.GetAxis(horizontalInput), 
            Input.GetAxis(verticalInput));
    }
    
    /**
     * Tells if the player is pressing the button "AddSphere"
     * @return True or false
     */
    public bool IsAddingSphere()
    {
        return Input.GetButton(addSphereInput);
    }

    /**
     * Tells if the player is pressing the button "RemoveSphere"
     * @return True or false
     */
    public bool IsRemovingSphere()
    {
        return Input.GetButton(removeSphereInput);
    }

    /**
     * Tells if the player is pressing the button "IncreaseRadius"
     * @return True or false
     */
    public bool IsIncreasingRadius()
    {
        return Input.GetButton(increaseRadiusInput);
    }

    /**
     * Tells if the player is pressing the button "ReverseRotation"
     * @return True or false
     */
    public bool IsReversingRotation()
    {
        return Input.GetButton(reverseRotationInput);
    }
}
