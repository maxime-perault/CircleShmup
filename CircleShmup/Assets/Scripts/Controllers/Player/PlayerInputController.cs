using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Manages inputs for the Player controller
 * @class PlayerInputController
 */
public class PlayerInputController : MonoBehaviour
{
    public bool canAddSphere       = true;
    public bool canRemoveSphere    = true;
    public bool canIncreaseRadius  = true;
    public bool canReverseRotation = true;

    [SerializeField] public string verticalInput        = "Vertical";
    [SerializeField] public string horizontalInput      = "Horizontal";
   
    [SerializeField] public string addSphereInput       = "AddSphere";
    [SerializeField] public string removeSphereInput    = "RemoveSphere";
    [SerializeField] public string increaseRadiusInput  = "IncreaseRadius";
    [SerializeField] public string reverseRotationInput = "ReverseRotation";
    
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
        return (canIncreaseRadius) ? Input.GetButton(increaseRadiusInput) : false;
    }

    /**
     * Tells if the player is pressing the button "ReverseRotation"
     * @return True or false
     */
    public bool IsReversingRotation()
    {
        return (canReverseRotation) ? Input.GetButton(reverseRotationInput) : false;
    }
}
