using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchScenes : MonoBehaviour {

	public Camera mainCamera;
	public Camera contestCamera;

	public GameObject mainCanvas;
	public GameObject contestCanvas;
	public UnityEngine.UI.Image transition; 
	public AudioSource flySound;

	public float zoomDuration = 2;
	public float zoomAnimEnd = 60;
	private float zoomStart;
	public float moveOffset = 0.2f;
	public float outAnimDuration = 0.5f;

	bool mainSceneActive = true;

	private static SwitchScenes main;

	// Use this for initialization
	void Start () {
		main = this;
		zoomStart = mainCamera.fieldOfView;
	}
	
	public static SwitchScenes Get() {
		return main;
	}

	public void ActiveScene(bool isMainScene){
		mainSceneActive = isMainScene;
		mainCamera.gameObject.SetActive(mainSceneActive);
		mainCanvas.SetActive(mainSceneActive);
		contestCamera.gameObject.SetActive(!mainSceneActive);
		contestCanvas.SetActive(!mainSceneActive);
	}

	public void ActiveMainScene() {
		ActiveScene(true);
		mainCamera.GetComponent<ViewDrag>().enabled = true;
		mainCamera.GetComponent<Camera>().enabled = true;
		mainCamera.GetComponent<AudioSource>().Play();
		mainCamera.fieldOfView = zoomStart;
	}

	bool doingSwitchAnimation = false;

	public void ActiveContestScene(string contestName, GameObject thing) {
		if(doingSwitchAnimation)
			return;

		flySound.Play();
		mainCamera.GetComponent<ViewDrag>().enabled = false;
		StartCoroutine(CameraZoomAnimation(contestName, thing));
	}

	private IEnumerator CameraZoomAnimation(string contestName, GameObject thing){
		doingSwitchAnimation = true;
		
		float time = 0;
		Vector3 startPosition = mainCamera.transform.position;
		Vector3 finalPosition = thing.transform.position;
		while(time < zoomDuration) {
			yield return new WaitForFixedUpdate();
			time += Time.fixedDeltaTime;
			float progress = time / (zoomDuration + moveOffset);

			//mainCamera.fieldOfView = Mathf.Lerp(zoomStart, zoomAnimEnd, progress);
			Vector3 cameraPos = Vector3.Slerp(startPosition, finalPosition, progress);
			transition.color = Color.Lerp(new Color(1, 1, 1, 0), Color.white, Mathf.Max(0, 2 * time / zoomDuration - 1));
			mainCamera.transform.position = cameraPos;
		}
		mainCamera.GetComponent<AudioSource>().Stop();
		ActiveScene(false);
		time = 0;
		while(time < outAnimDuration) {
			yield return new WaitForFixedUpdate();
			time += Time.fixedDeltaTime;
			transition.color = Color.Lerp(Color.white, new Color(1, 1, 1, 0), Mathf.Max(0, time / outAnimDuration));
		}
		ContestGame contest = ContestGame.Get();
		contest.StartContest(contestName, thing);

		doingSwitchAnimation = false;
	}

}
