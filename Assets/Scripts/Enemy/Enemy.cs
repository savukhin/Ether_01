using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character {
	public GameObject TargetPoint;

	// Use this for initialization
	void Start () {
		Untarget();
		StatusInitializate();
	}

	public void Target() {
		TargetPoint.SetActive(true);
	}

	public void Untarget() {
		TargetPoint.SetActive(false);
	}

	protected override void Death() {
		print(name + " Killed");
		model.GetComponent<CharacterAnimationController>().Death();
		Untarget();
		this.gameObject.SetActive(false);
		Destroy(this.gameObject);
	}

	// Update is called once per frame
	void Update () {
		
	}
}
