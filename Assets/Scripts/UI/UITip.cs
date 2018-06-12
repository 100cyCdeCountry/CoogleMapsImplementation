using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITip : MonoBehaviour {

	Text tipText;
	Animation anim;
	AudioSource source;
	static UITip main;
	static string tipMsg = "";

	// Use this for initialization
	void Start () {
		tipText = GetComponentInChildren<Text>();
		anim = GetComponent<Animation>();
		source = GetComponent<AudioSource>();
		main = this;
	}
	
	public static void SetTip(string msg) {
		tipMsg = msg;
	}

	private void ShowTip(string msg){
		main.anim.Play();
		source.Play();
		main.tipText.text = msg;
	}

	public static UITip Get() {
		return main;
	}

	public static void Hide() {
		float x = -212;
		main.GetComponent<RectTransform>().position = new Vector3(x, 5, 0);
	}

	void Update() {
		if(tipMsg != "") {
			ShowTip(tipMsg);
			tipMsg = "";
		}
	}

}
