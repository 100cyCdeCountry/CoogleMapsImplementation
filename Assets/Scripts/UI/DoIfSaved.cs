using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoIfSaved : MonoBehaviour {

	public string key;
	public UnityEvent saved;
	public UnityEvent notSaved;

	// Use this for initialization
	void Start () {
		if(PlayerPrefs.HasKey(key)) {
			if(saved != null)
				saved.Invoke();
		}else{
			if(notSaved != null)
				notSaved.Invoke();
		}
	}

	public void Save(string key) {
		PlayerPrefs.SetString(key, "");
		PlayerPrefs.Save();
	}
	
}
