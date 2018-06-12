using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WebGLPerformance : MonoBehaviour {

	#if UNITY_WEBGL
	// Use this for initialization
	void Awake () {
		Application.targetFrameRate = -1;
	}

	void Update() {
		if(Input.GetKeyDown(KeyCode.F5)) {
			SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
		}
	}
	#endif
	
}
