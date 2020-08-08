using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SpellIcon : MonoBehaviour {
    public GameObject imageSpot;
    public GameObject dark;
    public Text coolDownRemainText;
    public Spell spell;
    private bool isCoolDown;

    public void Initializate() {
        isCoolDown = false;
        dark.SetActive(false);
        coolDownRemainText.text = "";
        if (spell != null) {
            imageSpot.GetComponent<RawImage>().texture = spell.spellIcon;
        }
    }

    // Update is called once per frame
    void Update() {
        if (spell == null)
            return;
        if (spell.IsCoolDown()) {
            GoCoolDown();
        } else if (isCoolDown) {
            EndCoolDown();
        }
    }

    public void GoCoolDown() {
        dark.SetActive(true);
        isCoolDown = true;
        coolDownRemainText.text = String.Format("{0:0.00}", spell.RemainCoolDownTime());
    }

    public void EndCoolDown() {
        isCoolDown = false;
        dark.SetActive(false);
        coolDownRemainText.text = "";
    }
}
