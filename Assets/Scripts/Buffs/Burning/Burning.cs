using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burning : Buff {
    public int damagePerTime = 1;
    public float frequency = 1f; //In seconds
    private float lastTime;

    public override void BuffStart(Character sub) {
        Initializate(sub);
        lastTime = startTime;
    }

    public override void BuffAction() {
        float multiple = time / (lastTime + frequency);
        if (multiple >= 1) {
            subject.IncreaseHealthPoints(-(damagePerTime * ((int)multiple)));
            lastTime = lastTime + frequency * ((int)multiple);
        }
    }
}
