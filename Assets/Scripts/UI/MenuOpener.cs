using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuOpener : MonoBehaviour {

	private Animation anim;
	private AnimationClip openAnim;
	private AnimationClip closeAnim;

	void Start() {
		anim = GetComponent<Animation>();
		Vector3 position = GetComponent<RectTransform>().position;
		position.x = -400.0f;
		GetComponent<RectTransform>().position = position;
	}

	public void OpenMenu () {
		anim.clip = anim.GetClip("OpenMenu");
		anim.Play();
	}
	
	public void CloseMenu() {
		anim.clip = anim.GetClip("CloseMenu");
		anim.Play();
	}

}
