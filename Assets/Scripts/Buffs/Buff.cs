using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour {
    public float duration;
    public float startTime;
    public float time;
    public Character subject;
    public GameObject visualEffect_prefab;
    private GameObject visualEffect;

    // Update is called once per frame
    public void Update() {
        time = Time.time;
        BuffAction();
        if (time > duration + startTime) {
            BuffEnd();
        }        
    }

    public virtual void BuffStart(Character sub) {
        Initializate(sub);
    }
    protected void Initializate(Character sub) {
        startTime = Time.time;
        subject = sub;
        visualEffect = Instantiate(visualEffect_prefab, subject.transform.position, Quaternion.identity, subject.transform);
        
    }
    public virtual void BuffAction() {}
    public virtual void BuffEffect() {}

    public void BuffEnd() {
        Destroy(visualEffect);
        Destroy(this);
        Destroy(this.gameObject);
    }
}
