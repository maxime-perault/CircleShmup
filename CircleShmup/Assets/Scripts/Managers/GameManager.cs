using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Main game manager, shares data between scenes
 * stores game states
 * @class GameManager
 */
public class GameManager : MonoBehaviour
{
    /**
     * Makes the manager non-destroyable on scene loadings
     */
    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    void Start ()
    {
		// None
	}
	
	void Update ()
    {
		// None
	}
}
