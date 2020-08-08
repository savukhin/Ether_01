using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallProperties : SpellProperties {

    public float speed = 3f;
    public int damage = 10;

    public override void Action(Character subject) {
        subject.IncreaseHealthPoints(-damage);
    }

    public override void Triggered() {
        this.Destroy();
    }

    // Update is called once per frame
    void Update() {
        Vector3 forward = new Vector3(0, 0, 1f);
		transform.Translate(forward * speed * Time.deltaTime);
    }
}
