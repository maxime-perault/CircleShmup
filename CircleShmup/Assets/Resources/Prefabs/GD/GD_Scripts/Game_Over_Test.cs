using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Over_Test : MonoBehaviour {

	public Transform Player;
	private int hitpoints = 5;
	private Rect monrect = new Rect (0,0,100,100);
	private string vie = "";
	private GUIStyle guiStyle = new GUIStyle();

	// Use this for initialization
	void Start () {
		
		guiStyle.fontSize = 200; //change the font size
		guiStyle.normal.textColor = Color.white;
		hitpoints = GetComponent<PlayerController> ().hitPoint;

	}
	
	// Update is called once per frame
	void Update () {

		hitpoints = GetComponent<PlayerController> ().hitPoint;
		vie = ("" + hitpoints);
		Debug.Log (hitpoints);

		if (hitpoints <= 0)
			SceneManager.LoadScene (SceneManager.GetActiveScene().name);
	}

	void OnGUI () {
		GUI.Label(monrect, vie, guiStyle);
	}
}
