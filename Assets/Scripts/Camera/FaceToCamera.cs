using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceToCamera : MonoBehaviour {
	
	// Update is called once per frame
	void Start () {
		StartCoroutine(AfterStart());
	}

	IEnumerator AfterStart() {
		yield return new WaitForEndOfFrame();
		if(Camera.main != null)
			transform.rotation = Camera.main.transform.rotation;
	}
	
	void Update() {
		if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) 
			transform.rotation = Camera.main.transform.rotation;
	}

}
