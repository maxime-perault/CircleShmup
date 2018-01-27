using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePoele : MonoBehaviour {

	public PlayerInputController inputController;

	public float maxRotX, maxRotY = 8f; // base 8
	public float lerpSpeed = 0.03f; // base 0,03

	private float poeleX, poeleY;
	private Vector2 playerInput;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		// gere l'effet rotation de la poele avec l'input du joystick.
		if (Time.deltaTime != 0) {
			poeleX = 0;
			poeleY = 0;

			playerInput = inputController.GetAxis ();
			// Debug.Log (playerInput);

			poeleX = playerInput.x * maxRotX;
			poeleY = playerInput.y * maxRotY * -1f;

			// this.transform.rotation = Quaternion.Euler (poeleY, poeleX, 0f);
			this.transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.Euler (poeleY, poeleX, 0f), lerpSpeed);
		}
		
	}
}
