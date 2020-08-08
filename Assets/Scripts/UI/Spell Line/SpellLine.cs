using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellLine : MonoBehaviour {
    public Character character;
    public GameObject spellIcon_prefab;
    public GameObject[] spellIcons;
    private Spell[] spells;
    public float width = 0.2f;
    public float startPosition = 50f;

    // Start is called before the first frame update
    void Start() {
        spellIcons = new GameObject[9];
        spells = character.spells;
        startPosition = 85f;
        width = 7f;
        for (int i = 0; i < 9; i++) {
            Vector3 iconPosition = transform.position;
            spellIcons[i] = Instantiate(spellIcon_prefab, iconPosition, Quaternion.identity, transform);
            iconPosition.x = startPosition + (width + spellIcons[i].GetComponent<RectTransform>().rect.width)* i;
            spellIcons[i].transform.position = iconPosition;
            if (i + 1 <= spells.Length) {
                spellIcons[i].GetComponent<SpellIcon>().spell = spells[i];                
            }
            spellIcons[i].GetComponent<SpellIcon>().Initializate();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
