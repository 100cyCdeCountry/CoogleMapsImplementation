using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnPressKey : MonoBehaviour {

	public UnityEvent onKeyEvent;
	public bool onlyFirst = true;
	public KeyCode key;
	
	bool casted = false;

	void Start() {
		if (onKeyEvent == null)
            onKeyEvent = new UnityEvent();
	}

	void Update()
	{
		if(Camera.main == null) return;

		if(Input.GetKeyDown(key) && (!onlyFirst || !casted) ) {
			onKeyEvent.Invoke();
			casted = true;
		}
	}

	public void DebugMsg(string s) {
		Debug.Log(s);
	}

	public void PlayAnimation() {
		GetComponent<Animation>().Play();
	}

	public void PlayParticleSystem() {
		GetComponent<ParticleSystem>().Play();
	}

}
