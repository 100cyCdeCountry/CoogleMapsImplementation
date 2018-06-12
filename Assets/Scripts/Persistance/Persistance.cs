using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Persistance : MonoBehaviour {

	public void ResetGame() {
		PlayerPrefs.DeleteAll();
		SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
	}

}
