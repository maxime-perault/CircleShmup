using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Manages inputs for the Player controller
 * @class PlayerInputController
 */
public class PlayerInputController : MonoBehaviour
{
    [SerializeField] private string verticalInput       = "Vertical";
    [SerializeField] private string horizontalInput     = "Horizontal";
   
    [SerializeField] private string addSphereInput      = "AddSphere";
    [SerializeField] private string removeSphereInput   = "RemoveSphere";
    [SerializeField] private string increaseRadiusInput = "IncreaseRadius";
    [SerializeField] private string invertRotationInput = "InvertRotation";
    
    private PlayerController playerControllerInstance;
	
    /**
     * Initializes the input controller by finding
     * the player controller
     */
	void Start ()
    {
        playerControllerInstance = GetComponent<PlayerController>();

        if(!playerControllerInstance)
        {
            Debug.LogError("Unable to find the PlayerController component");
        }
    }
	
    /**
     * Called on each update, listens inputs
     */
	void Update ()
    {
        Vector2 axis = new Vector2();
        axis.x = Input.GetAxis(horizontalInput);
        axis.y = Input.GetAxis(verticalInput);

        bool addSpherePressed      = Input.GetButton(addSphereInput);
        bool removeSpherePressed   = Input.GetButton(removeSphereInput);
        bool increaseRadiusPressed = Input.GetButton(increaseRadiusInput);
        bool revertRotationPressed = Input.GetButton(invertRotationInput);

        playerControllerInstance.Move(axis, 
            addSpherePressed,      removeSpherePressed, 
            increaseRadiusPressed, revertRotationPressed);
    }
}
