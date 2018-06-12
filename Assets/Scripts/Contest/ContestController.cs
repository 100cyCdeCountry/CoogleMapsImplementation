using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Fix this mess
public class ContestController : MonoBehaviour, IContestDisplay {
	
	private GameObject contestGuy;
	private GameObject realGuy;
	private IContestData currentContest;
	private IContestSentence currentSentence;
	
	public Text questionDisplay;
	public Button[] answersButtons;
	public Transform guyPosition;
	public Animation showButtonsAnimation;
	public float timeBetweeLetter = 0.1f;

	public AudioSource winSound;
	public AudioSource failSound;

	private bool onContest = false;
	private ColorBlock initColors;
	private int lastAnswer;
	private string writing = "";
	

	// Use this for initialization
	void Start () {
		InitController();
	}

    private void InitController()
    {
		for(int i = 0; i < ContestData.numberOfAnswers; i++) {
			int n = i;
			answersButtons[i].onClick.AddListener(delegate { SelectOption(n); });
		}
		initColors = answersButtons[0].colors;
    }

	public void StartContest(IContestData contest, GameObject guy) {
		StartContest(contest, guy, Quaternion.identity, Vector3.one, Vector3.zero);
	}

	public void StartContest(IContestData contest, GameObject guy,
							Quaternion rotation, Vector3 scale,
							Vector3 offset) {
		onContest = true;
		currentContest = contest;
		currentContest.Restart();
		currentSentence = contest.Begin();
		lastAnswer = -1;

		contestGuy = Instantiate(guy, guyPosition.position + offset, rotation);
		Destroy(contestGuy.GetComponent<ContestInitiator>());
		contestGuy.transform.localScale = scale;
		realGuy = guy;

		currentSentence.ShowIn(this);
	}
	
	public void SelectOption(int option) {
		lastAnswer = option;
		MoveNext();
	}

	public void ExitContest() {
		HideButtons();
		WriteAll("");
		if(contestGuy != null)
			Destroy(contestGuy);

		onContest = false;
	}

	Coroutine writeCoroutine;
	public void Write(string text){
		if(writeCoroutine != null)
			StopCoroutine(writeCoroutine);

		writeCoroutine = StartCoroutine(WriteAnimation(text));
	}

	private void WriteAll(string text) {
		if(writeCoroutine != null)
			StopCoroutine(writeCoroutine);
		
		writeCoroutine = null;
		questionDisplay.text = text;
	}

	private IEnumerator WriteAnimation(string text) {
		writing = text;
		for(int i = 0; i <= text.Length; i++) {
			yield return new WaitForSeconds(timeBetweeLetter);
			questionDisplay.text = text.Substring(0, i) + "<color=#00000000>" + text.Substring(i) + "</color>";
		}
		writeCoroutine = null;
	}

	public void FinishGame() {
		realGuy.GetComponent<ContestInitiator>().MarkAsFinished();
		currentContest.OnEnd();
		ExitContest();
	}

	
	bool IsWriting() {
		return writeCoroutine != null;
	}

    void Update() {
		if(onContest && (Input.anyKeyDown || Input.GetMouseButtonDown(0))) {
			TryToMoveNext();
		}
	}

    private void TryToMoveNext()
    {
		if(IsWriting()) {
			WriteAll(writing);
		}else if(!currentSentence.WaitForButtons()) {
			MoveNext();
		}
    }

    public void ShowButtons(string[] solutions)
    {
        for(int i = 0; i < ContestData.numberOfAnswers; i++) {
			answersButtons[i].gameObject.SetActive(true);
			answersButtons[i].interactable = true;
			SetButtonText(i, solutions[i]);
			answersButtons[i].colors = initColors;
			answersButtons[i].GetComponent<Image>().color = Color.white;
		}
		showButtonsAnimation.Play();
    }

    public void DisableButtons()
    {
        for(int i = 0; i < ContestData.numberOfAnswers; i++) {
			answersButtons[i].interactable = false;
		}
    }

    public void HideButtons()
    {
        for(int i = 0; i < ContestData.numberOfAnswers; i++) {
			answersButtons[i].gameObject.SetActive(false);
		}
    }

    public int GetLastAnswer()
    {
        return lastAnswer;
    }

    public void MoveNext()
    {
        currentSentence = currentSentence.GoToNextSentence(this);
		if(currentSentence == null) {
			FinishGame();
			SwitchScenes.Get().ActiveMainScene();
		}else{
			currentSentence.ShowIn(this);
		}
    }

    public void SetColorButton(int i, ButtonState state)
    {
		Color buttonColor;
		switch(state) {
			case ButtonState.Correct:
				buttonColor = Color.green;
			break;
			case ButtonState.Fail:
				buttonColor = Color.red;
			break;
			default:
				buttonColor = Color.white;
			break;
		}
        ColorBlock c = answersButtons[i].colors;
		c.normalColor = buttonColor;
		answersButtons[i].colors = c;
		answersButtons[i].GetComponent<Image>().color = buttonColor;
    }

    public void SetButtonText(int i, string text)
    {
        answersButtons[i].GetComponentInChildren<Text>().text = text;
    }

    public GameObject GetCharacter()
    {
        return contestGuy;
    }

    public void PlayWin()
    {
        winSound.Play();
    }

    public void PlayFail()
    {
        failSound.Play();
    }
}
