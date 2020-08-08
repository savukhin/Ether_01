using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cure : Spell {
    public GameObject cure;
    public int recover;
    public override void ActivateSpell(Character subject) {
        cure.GetComponent<CureProperties>().recover = recover;
        Instantiate(cure, subject.transform.position, subject.model.transform.rotation);        
        subject.IncreaseHealthPoints(recover);
    }
}
