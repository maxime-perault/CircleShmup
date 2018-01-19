using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Main game manager, shares data between scenes
 * stores game states
 * This class is a singleton non destroyable on LoadScene
 * @class GameManager
 */
public class GameManager : MonoBehaviour
{
    private static GameManager GMref;

    void Awake()
    {
        if (GMref == null)
        {
            GMref = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(gameObject);
        }
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
