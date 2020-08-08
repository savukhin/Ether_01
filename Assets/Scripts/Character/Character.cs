using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {
	[SerializeField] private LayerMask platformLayerMask;
	public GameObject model;
	public float moveSpeed = 2f;
	public Weapon weapon;
	public int healthPointsMax = 50;
	public int healthPoints = 20;
	public int etherPointsMax = 100;
	public int etherPoints = 80;
	public bool knockedDown = false;
	protected bool isOnGround = false;	
	public Spell[] spells;
	public HashSet<Buff> buffs = new HashSet<Buff>();

	public bool isGrounded(float extraDimension = 0.1f) {
		RaycastHit temp;
		return Physics.BoxCast(model.GetComponent<Collider>().bounds.center, model.GetComponent<Collider>().bounds.size / 2 - new Vector3(extraDimension, extraDimension, extraDimension), new Vector3(1f, -1f, 1f), out temp, model.transform.rotation, 2 * extraDimension, platformLayerMask.value);
	}

	protected void StatusInitializate() {
		foreach(Spell s in spells) {
			s.lastUse = -100f;
		}
		
		foreach(Buff b in buffs) {
			if (b != null)
				b.GetComponent<Buff>().BuffStart(this);
		}
	}

	public void BuffAdd(Buff buff_prefab) {
		Buff buff = Instantiate(buff_prefab, transform.position, transform.rotation, transform);
		buff.BuffStart(this);
		buffs.Add(buff);
	}

	protected void BuffUpdate() {
		foreach(Buff b in buffs) {
			if (b != null) {
				b.Update();
			} else {
				buffs.Remove(b);
			}
		}
	}
	
	protected virtual void takeDamage(int damage) {
		healthPoints -= damage;
	}

	public void OnTriggerEnter(Collider other) {
		if (other.tag == "Weapon") {
			HittedByWeapon(other.GetComponent<Weapon>());
		} else if (other.tag == "Spell") {
			HittedBySpell(other.GetComponent<SpellProperties>());
		}
	}

	protected void HittedByWeapon(Weapon other_weapon) {
		if (other_weapon.attack == true) {				
				if (other_weapon.tag != tag) {					
					if (!knockedDown) {
						takeDamage(other_weapon.damage);
						StartCoroutine (KnockDown());
					}
				}
				if (healthPoints <= 0) {
					Death();
				}
			}
	}
	
	protected void HittedBySpell(SpellProperties other_spellProperties) {
		if (other_spellProperties.ownerTag == tag)
			return;
		other_spellProperties.Action(this);
		Buff[] newBuffs = other_spellProperties.GetBuffs();
		foreach (Buff buff in newBuffs) {
			BuffAdd(buff);
		}
		other_spellProperties.Triggered();
	}
	
	public virtual void IncreaseHealthPoints(int count) {
		healthPoints = Mathf.Min(count + healthPoints, healthPointsMax);
		if (healthPoints <= 0) 
			Death();
	}

	public virtual void IncreaseEtherPoints(int count) {
		etherPoints = Mathf.Min(count + etherPoints, etherPointsMax);
	}

	public IEnumerator KnockDown() {
		knockedDown = true;
		CharacterAnimationController anim = model.GetComponent<CharacterAnimationController>();
		Animator a = anim.anim;
		anim.KnockDown();
		float dT = Time.deltaTime;
		yield return new WaitForSecondsRealtime(dT);
		yield return new WaitForSeconds(a.GetCurrentAnimatorStateInfo(0).length - dT);
		knockedDown = false;
		
	}

	protected virtual void Death() {
		print(name + " Killed");
		model.GetComponent<CharacterAnimationController>().Death();
		Destroy(this);
	}

	protected virtual void LowEtherMessage() {
		//print("Not enough ether");
	}

	protected void Spell (int spellNumber) {		
		if (etherPoints < spells[spellNumber].ehterPointsCost) {
			LowEtherMessage();
			return;
		}
		if (spells[spellNumber].IsCoolDown()) {
			//print("CoolDown!!!");
			return;
		}
		spells[spellNumber].lastUse = Time.time;
		spells[spellNumber].ActivateSpell(this);
		etherPoints -= spells[spellNumber].ehterPointsCost;
	}
}
