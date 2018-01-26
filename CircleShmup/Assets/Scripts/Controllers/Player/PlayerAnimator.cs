using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour {

    Animator animator;
    SpriteRenderer sprite;

	// Use this for initialization
	void Start () {
        animator = this.gameObject.GetComponent<Animator>();
        sprite = this.gameObject.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
       
	}

    public void Right()
    {
        sprite.flipX = false;
    }

    public void Left()
    {
        sprite.flipX = true;
    }
}
