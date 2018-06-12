using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnObserve : MonoBehaviour {

	public UnityEvent onObserveEvent;
	public bool onlyFirst = true;
	public float minDistance = Mathf.Infinity;

	bool observed = false;

	void Start() {
		if (onObserveEvent == null)
            onObserveEvent = new UnityEvent();
	}

	void Update()
	{
		if(Camera.main == null) return;

		Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
 		bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 &&
		  screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
		if(onScreen && (!onlyFirst || !observed) 
			&& Vector3.Distance(Camera.main.transform.position, transform.position) < minDistance) {
			onObserveEvent.Invoke();
			observed = true;
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
