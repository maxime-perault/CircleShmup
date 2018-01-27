using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour {

    Animator animator;
    SpriteRenderer sprite;

    private int PlayerLife = 6;

	// Use this for initialization
	void Start () {
        animator = this.gameObject.GetComponent<Animator>();
        sprite = this.gameObject.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {

        switch (PlayerLife)
        {
            case 0:
                animator.SetBool("Low", false);
                animator.SetBool("Middle", false);
                animator.SetBool("Neutre", false);
                animator.SetBool("High", true);
                break;
            case 1:
                animator.SetBool("Low", false);
                animator.SetBool("Middle", false);
                animator.SetBool("Neutre", false);
                animator.SetBool("High", true);
                break;
            case 2:
                animator.SetBool("Low", false);
                animator.SetBool("High", false);
                animator.SetBool("Neutre", false);
                animator.SetBool("Middle", true);
                break;
            case 3:
                animator.SetBool("Middle", false);
                animator.SetBool("High", false);
                animator.SetBool("Neutre", false);
                animator.SetBool("Low", true);
                break;
            case 4:
                animator.SetBool("Middle", false);
                animator.SetBool("High", false);
                animator.SetBool("Neutre", false);
                animator.SetBool("Low", true);
                break;
            case 5:
                animator.SetBool("Middle", false);
                animator.SetBool("High", false);
                animator.SetBool("Low", false);
                animator.SetBool("Neutre", true);
                break;
            case 6:
                animator.SetBool("Middle", false);
                animator.SetBool("High", false);
                animator.SetBool("Low", false);
                animator.SetBool("Neutre", true);
                break;
        }

    }

    public void Right()
    {
        sprite.flipX = false;
    }

    public void Left()
    {
        sprite.flipX = true;
    }

    public void SetLife(int life)
    {
        PlayerLife = life;
    }
}
