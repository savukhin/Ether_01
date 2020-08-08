using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellProperties : MonoBehaviour {
    public Buff[] buff_prefabs;
    public string ownerTag = "Player";

    public virtual void Action(Character subject) {}

    public virtual Buff[] GetBuffs() {
        return buff_prefabs;
    }

    public virtual void Triggered() {
        this.Destroy();
    }

    public void Destroy() {
        Destroy(this.gameObject);
        Destroy(this);
    }
}
