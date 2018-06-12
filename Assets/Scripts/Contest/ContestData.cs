using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IContestData
{
	IContestSentence Begin();
	void AddAnswer(bool correct);
	bool WasAnswerCorrect(int i);
	bool HaveWin();
	void Restart();
	void OnEnd(Action<IContestData> f);
	void OnEnd();
}


public struct ContestAnswer{
	public string text;
	public string[] corrections;
	public bool correct;
}

public class ContestData : IContestData {

	public static int numberOfAnswers = 3;

	private IContestSentence start;
	private bool win = false;
	private Action<IContestData> onEnd;

	private static bool[] GetCorrectionArray(int i) {
		bool[] a = new bool[3]{false, false, false};
		a[i] = true;
		return a;
	}

	public ContestData(string question, string[] answers, string[][] corrections,
							 int correct, string[] sentences = null, string[] finalSentences = null)
							 : this(question, answers, corrections, 
							 GetCorrectionArray(correct)
							 , sentences, finalSentences)
							 {	}


	public ContestData(string question, string[] answers, string[][] corrections,
							 bool[] correct, string[] sentences = null, string[] finalSentences = null) {
		
		IContestSentence[] correctionsSentences = new IContestSentence[numberOfAnswers];
		IContestSentence final = finalSentences == null? null : SentenceListWithEffects(finalSentences);
		for(int i = 0; i < numberOfAnswers; i++) {
			correctionsSentences[i] = SentenceListWithEffects(corrections[i], final);
		}

		ContestQuestion q = new ContestQuestion(this, question, answers, correct, correctionsSentences);
		start = SentenceListWithEffects(sentences, q);
	}

    public IContestSentence Begin()
    {
        return start;
    }

    public void AddAnswer(bool correct)
    {
        win = correct;
    }

    public bool WasAnswerCorrect(int i)
    {
        return win;
    }

    public bool HaveWin()
    {
        return win;
    }

    public void Restart()
    {
        win = false;
    }

	public void OnEnd(Action<IContestData> f)
    {
        onEnd = f;
    }

	public void OnEnd() {
		onEnd(this);
	}

	public static IContestSentence SentenceListWithEffects(string[] sentences, IContestSentence nextSentence = null) {
		IContestSentence next = nextSentence;
		for(int i = sentences.Length - 1; i >= 0; i--) {
			IContestSentence s;
			if(sentences[i].Length == 0 || sentences[i][0] != '#') {
				s = new Sentence(sentences[i], next);
			}else{
				s = new EffectSentence(sentences[i].Substring(1), next);
			}
			next = s;
		}
		return next;
	}

}


public class ContestDefault : IContestData
{
	IContestSentence start;
	Func<ContestDefault, bool> haveWin;
	Func<bool> addAnswer;
	List<bool> corrects;
	Action<IContestData> onEnd;

    public ContestDefault(IContestSentence startSentece, Func<ContestDefault, bool> haveWin)
    {
		this.corrects = new List<bool>();
		this.onEnd = _ => {};
		this.start = startSentece;
		this.haveWin = haveWin;
    }

	public ContestDefault(Func<ContestDefault, bool> haveWin)
	: this(null, haveWin)
    { }

	public ContestDefault(IContestSentence startSentece = null)
	: this(startSentece, _ => true)
    { }

	public void SetSentences(IContestSentence start) {
		this.start = start;
	}

    public void AddAnswer(bool correct)
    {
        corrects.Add(correct);
    }

    public IContestSentence Begin()
    {
        return start;
    }

    public bool HaveWin()
    {
        return haveWin(this);
    }

    public void Restart()
    {
        corrects.Clear();
    }

    public bool WasAnswerCorrect(int i)
    {
        return corrects[i];
    }

    public void OnEnd(Action<IContestData> f)
    {
        onEnd = f;
    }

	public void OnEnd() {
		onEnd(this);
	}

}
