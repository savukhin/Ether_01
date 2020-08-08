using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour {
	public Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");
		float speed = Mathf.Sqrt(h * h + v * v);
		anim.SetFloat("Speed", speed);
	}

	public void Jump() {
		anim.SetTrigger("Jump");
		anim.SetBool("Grounded", false);
	}

	public void Grounding() {
		anim.SetBool("Grounded", true);
	}

	public void Hit() {
		anim.SetTrigger("Hit");
	}

	public void KnockDown() {
		anim.SetTrigger("KnockDown");
	}
}
