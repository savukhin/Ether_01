using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ThirdPersonController : Character {
	public GameObject main_camera;
	private Rigidbody rb;
	public BarController healthPointsBar;
	public BarController etherPointsBar;
	public float jumpForce = 8.3f;
	public GameObject target = null;
	private bool targeting = false;
	private bool alive = true;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		healthPointsBar.maxState = healthPointsMax;
		healthPointsBar.UpdateState(healthPoints);
		etherPointsBar.maxState = etherPointsMax;
		etherPointsBar.UpdateState(etherPoints);
		StatusInitializate();
/*
		HashSet<int> temp = new HashSet<int>();
		temp.Add(1);
		temp.Add(2);
		int[] temp_2 = new int[temp.Count];
*/	
	}

	// Update is called once per frame
	void Update () {
		if (!alive)
			return;
		BuffUpdate();
		if (targeting == true && target == null) {
			ClearTarget();
		}
		if (!isOnGround) {
			if (isGrounded()) {
				isOnGround = true;
				model.GetComponent<CharacterAnimationController>().Grounding();
			}
		}
		if (weapon.attack == false) {
			MoveAndRotate();
			if (Input.GetKeyDown(KeyCode.Space) && isGrounded()) {
				isOnGround = false;
				Jump();
			}
		}
		if (Input.GetKeyDown(KeyCode.Tab)) {
			SearchTarget();
			SetOnTarget();
		}
		if (targeting && Input.GetKeyDown(KeyCode.Escape)) {
			print("Tilde");
			ClearTarget();
		}
		SpellCheck();
		Hit();
	}

	public override void IncreaseHealthPoints(int count) {
		healthPoints = Mathf.Min(count + healthPoints, healthPointsMax);
		healthPointsBar.UpdateState(healthPoints);
		if (healthPoints <= 0)
			Death();
	}
	
	public override void IncreaseEtherPoints(int count) {
		etherPoints = Mathf.Min(count + etherPoints, etherPointsMax);
		etherPointsBar.UpdateState(etherPoints);
	}

	protected override void LowEtherMessage() {
		print("Not enough ether");
	}

	private void SpellCheck() {
		if (Input.GetKeyDown(KeyCode.Alpha1) && spells.Length > 0) {
			Spell(0);
			etherPointsBar.UpdateState(etherPoints);
		} 
		if (Input.GetKeyDown(KeyCode.Alpha2) && spells.Length > 1) {
			Spell(1);
			etherPointsBar.UpdateState(etherPoints);
		}
		if (Input.GetKeyDown(KeyCode.Alpha3) && spells.Length > 2) {
			Spell(2);
			etherPointsBar.UpdateState(etherPoints);
		}
		if (Input.GetKeyDown(KeyCode.Alpha4) && spells.Length > 3) {
			Spell(3);
			etherPointsBar.UpdateState(etherPoints);
		}
		if (Input.GetKeyDown(KeyCode.Alpha5) && spells.Length > 4) {
			Spell(4);
			etherPointsBar.UpdateState(etherPoints);
		}
		if (Input.GetKeyDown(KeyCode.Alpha6) && spells.Length > 5) {
			Spell(5);
			etherPointsBar.UpdateState(etherPoints);
		}
		if (Input.GetKeyDown(KeyCode.Alpha7) && spells.Length > 6) {
			Spell(6);
			etherPointsBar.UpdateState(etherPoints);
		}
		if (Input.GetKeyDown(KeyCode.Alpha8) && spells.Length > 7) {
			Spell(7);
			etherPointsBar.UpdateState(etherPoints);
		}
		if (Input.GetKeyDown(KeyCode.Alpha9) && spells.Length > 8) {
			Spell(8);
			etherPointsBar.UpdateState(etherPoints);
		}
	}

	private void SetOnTarget() {
		targeting = true;
		main_camera.GetComponent<CameraMovement>().targetPosition = target.transform.position;
		main_camera.GetComponent<CameraMovement>().targeting = true;
		target.GetComponent<Enemy>().Target();
	}

	private void SearchTarget() {
		GameObject[] targets = GameObject.FindGameObjectsWithTag("Enemy");
		foreach (GameObject t in targets) {
			if (target != t) {
				if (target != null)
					target.GetComponent<Enemy>().Untarget();
				target = t;
				break;
			}
		}
	}

	private void ClearTarget() {
		if (target != null) {
			target.GetComponent<Enemy>().Untarget();
			target = null;
		}
		main_camera.GetComponent<CameraMovement>().CancelTargeting();
		targeting = false;
		
	}

	protected override void takeDamage(int damage) {
		healthPoints -= damage;
		healthPointsBar.UpdateState(healthPoints);
	}

	public void Hit() {
		if (Input.GetKeyDown(KeyCode.Mouse0)) {
			StartCoroutine (weapon.Hit(this));
		}
	}

	private void Jump() {
		rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
		model.GetComponent<CharacterAnimationController>().Jump();
	}

	private void MoveAndRotate () {
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");
		float angleToZ = main_camera.transform.eulerAngles.y * Mathf.Deg2Rad;
		float angleToX = (360 - main_camera.transform.eulerAngles.y) * Mathf.Deg2Rad;

		Vector3 direction = new Vector3((h*Mathf.Cos(angleToX) + v*Mathf.Sin(angleToZ)), 0f, (h*Mathf.Sin(angleToX) + v*Mathf.Cos(angleToZ))).normalized;
		rb.MovePosition(rb.position + direction * moveSpeed * Time.deltaTime);
		if (direction.magnitude > 0.3) {
			float newAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
			model.transform.rotation = Quaternion.Euler(0f, newAngle, 0f);
		} else if (targeting) {
			model.transform.rotation = Quaternion.Euler(0f, angleToZ * Mathf.Rad2Deg, 0f);
		}
	}

	protected override void Death() {
		print(name + " Killed");
		model.GetComponent<CharacterAnimationController>().Death();
		print("GAME OVER");
		alive = false;
	}
}
