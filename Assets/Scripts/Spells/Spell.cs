using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spell : MonoBehaviour {
    public int ehterPointsCost;
    public float coolDown;
    public float lastUse = -100f;
    public string ownerTag = "Player";
    public Texture spellIcon;

    public virtual void ActivateSpell(Character subject) {}

    public bool IsCoolDown () {
        float now = Time.time;
        return lastUse + coolDown > now;
    }

    public float RemainCoolDownTime() {
        float now = Time.time;
        return lastUse + coolDown - now;
    }
}
