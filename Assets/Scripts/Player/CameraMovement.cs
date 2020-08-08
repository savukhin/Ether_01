using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
	public float sensitivity = 4f;
	public bool targeting = false;
	public Vector3 targetPosition;
	public Vector3 selfPosition;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (targeting) {
			Vector3 selfPosition = transform.position;
			Vector3 targetToSelfVector = new Vector3(targetPosition.x - selfPosition.x, targetPosition.y - selfPosition.y, targetPosition.z - selfPosition.z);			
			transform.rotation =  Quaternion.Euler(0f, 
				Mathf.Atan2(targetToSelfVector.x, targetToSelfVector.z) * Mathf.Rad2Deg, 
				0f);
		} else {
			transform.Rotate(0f, Input.GetAxis("Mouse X") * sensitivity, 0f);
		}
	}

	public void CancelTargeting() {
		targeting = false;
	}
}
