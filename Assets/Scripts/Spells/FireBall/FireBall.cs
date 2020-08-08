using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : Spell {
    public int damage = 10;
    public float speed = 3f;
    public GameObject fireball_prefab;
    private GameObject fireball;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    public override void ActivateSpell(Character subject) {
        ehterPointsCost = 10;
        fireball_prefab.GetComponent<FireBallProperties>().damage = damage;
        fireball_prefab.GetComponent<FireBallProperties>().speed = speed;
        fireball_prefab.GetComponent<FireBallProperties>().ownerTag = ownerTag;
        Vector3 startPosition = subject.model.GetComponent<Collider>().bounds.center;
        startPosition.z += subject.model.GetComponent<Collider>().bounds.extents.z + fireball_prefab.GetComponent<SphereCollider>().radius;
        Instantiate(fireball_prefab, startPosition, subject.model.transform.rotation);
    }
}
