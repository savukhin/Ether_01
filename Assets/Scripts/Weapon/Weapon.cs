using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
	public int damage = 10;
	public string ownerTag = "Player";
	public bool attack = false;

	// Use this for initialization
	void Start () {
		
	}

	public IEnumerator Hit(Character subject) {
		attack = true;
		CharacterAnimationController anim = subject.model.GetComponent<CharacterAnimationController>();
		Animator a = anim.anim;
		anim.Hit();
		float dT = Time.deltaTime;
		yield return new WaitForSecondsRealtime(dT);
		yield return new WaitForSeconds(a.GetCurrentAnimatorStateInfo(0).length - dT);
		attack = false;
	}

	IEnumerator Wait (float count) {
		yield return new WaitForSecondsRealtime(10f);
	}

	// Update is called once per frame
	void Update () {
		
	}
}
