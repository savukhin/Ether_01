using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarController : MonoBehaviour {
    public float maxState = 10f;
    public float currentState = 3f;
    public GameObject bar;
    private float size = 400;
    // Start is called before the first frame update
    void Start() {
        size = GetComponent<RectTransform>().rect.width;
        //print(name + " size = " + size);
    }
    
    public void UpdateState(float newState) {
        currentState = newState;
        bar.transform.localScale = new Vector3(currentState / maxState, 1f, 1f);        
        bar.transform.localPosition = new Vector3(-size / 2 + currentState / maxState / 2 * size, 0f, 0f);
    }

    // Update is called once per frame
    void Update() {
        
    }
}
