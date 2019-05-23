using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContestInitiator : MonoBehaviour {

	public string contestName;
	public bool hideOnStart = false;
	private bool finished = false;

	void Start() {
		LoadSavedState();
		ContestGame.AddCharacter(gameObject, contestName);
		
		SaveState();

		if(hideOnStart)
			gameObject.SetActive(false);

	}

	void OnMouseDown()
	{
		if(!finished)
			SwitchScenes.Get().ActiveContestScene(contestName, gameObject);
	}

    public void MarkAsFinished(bool finish = true)
    {
        finished = finish;
		SaveState();
    }

	public void Show() {
		gameObject.SetActive(true);
		hideOnStart = false;
		SaveState();
	}

	public void Hide() {
		gameObject.SetActive(false);
		hideOnStart = true;
		SaveState();
	}

	private void LoadSavedState() {
		if(PlayerPrefs.HasKey(contestName + "Finished")) {
			finished = PlayerPrefs.GetInt(contestName + "Finished") == 1;
		}

		if(PlayerPrefs.HasKey(contestName + "HideOnStart")) {
			hideOnStart = PlayerPrefs.GetInt(contestName + "HideOnStart") == 1;
		}
	}

	private void SaveState() {
		PlayerPrefs.SetInt(contestName + "Finished", finished? 1 : 0); 
		PlayerPrefs.SetInt(contestName + "HideOnStart", hideOnStart? 1 : 0); 
		PlayerPrefs.Save();
	}

}
