using System;

public interface IContestSentence{
	IContestSentence GoToNextSentence(IContestDisplay display);
	void ShowIn(IContestDisplay display);
	bool WaitForButtons();
	void SetNext(IContestSentence next);
}


public abstract class CanGetNext{
	public abstract IContestSentence Next();

	public IContestSentence GetLast() {
		CanGetNext n = this;
		while(n.Next() != null) {
			if(n.Next() is CanGetNext) {
				n = (CanGetNext)n.Next();
			}else{
				return n.Next();
			}
		}
		return (IContestSentence)n;
	}
}

public class ContestDef : CanGetNext, IContestSentence
{
	private IContestSentence next;
	private Action<ContestDef, IContestDisplay> onShowIn;
	private Func<ContestDef, IContestDisplay, IContestSentence> onGoNext;

	public ContestDef(Action<ContestDef, IContestDisplay> onShowIn,
	 Func<ContestDef, IContestDisplay, IContestSentence> onGoNext,
	 IContestSentence next = null){
		this.next = next;
		this.onShowIn = onShowIn;
		this.onGoNext = onGoNext;
	}

    public IContestSentence GoToNextSentence(IContestDisplay display)
    {
        return onGoNext(this, display);
    }

    public void ShowIn(IContestDisplay display)
    {
		onShowIn(this, display);
    }

	public bool WaitForButtons()
    {
        return false;
    }
	
    public void SetNext(IContestSentence next)
    {
        this.next = next;
    }

    public override IContestSentence Next()
    {
        return next;
    }
}

public class Sentence : CanGetNext, IContestSentence
{
	private string sentence;
	private IContestSentence next;

	public Sentence(string sentence, IContestSentence next = null) {
		this.sentence = sentence;
		this.next = next;
	}

    public IContestSentence GoToNextSentence(IContestDisplay display)
    {
        return next;
    }

    public void ShowIn(IContestDisplay display)
    {
        display.Write(sentence);
    }

    public bool WaitForButtons()
    {
        return false;
    }

	public static Sentence CreateDialog(string[] sentences, IContestSentence nextSentence = null){
		IContestSentence next = nextSentence;
		for(int i = sentences.Length - 1; i >= 0; i--) {
			Sentence s = new Sentence(sentences[i], next);
			next = s;
		}
		return (Sentence)next;
	}

	public static IContestSentence GetLast(Sentence start) {
		Sentence n = start;
		while(n.next != null) {
			if(n.next is Sentence) {
				n = (Sentence)n.next;
			}else{
				return n.next;
			}
		}
		return n;
	}

    public void SetNext(IContestSentence next)
    {
        this.next = next;
    }

    public override IContestSentence Next()
    {
        return next;
    }
}


public class ContestHideButtons : CanGetNext, IContestSentence
{
	private IContestSentence next;

	public ContestHideButtons(IContestSentence next = null){
		this.next = next;
	}

    public IContestSentence GoToNextSentence(IContestDisplay display)
    {
        return next;
    }

    public void ShowIn(IContestDisplay display)
    {
		display.HideButtons();
        display.MoveNext();
    }

	public bool WaitForButtons()
    {
        return false;
    }
	
    public void SetNext(IContestSentence next)
    {
        this.next = next;
    }

    public override IContestSentence Next()
    {
        return next;
    }
}

public class ContestQuestion : IContestSentence
{
	private string question;
	private string[] answers = null;
	private bool[] solutions = null;
	private IContestSentence[] nexts = null;
	private IContestData contest;

    public ContestQuestion(IContestData contest, string question, string[] answers, int correct,
				 IContestSentence[] nexts = null) {
		this.question = question;
		this.answers = answers;
		this.solutions = new bool[3];
		for(int i = 0; i < 3; i++) {
			solutions[i] = correct == i;
		}
		this.nexts = nexts;
		this.contest = contest;
	}

	public ContestQuestion(IContestData contest, string question, string[] answers, bool[] solutions,
				 IContestSentence[] nexts = null)  {
		this.question = question;
		this.answers = answers;
		this.solutions = solutions;
		this.nexts = nexts;
		this.contest = contest;
	}

	public void Set(IContestSentence[] nexts) {
		this.nexts = nexts;
	}

    public IContestSentence GoToNextSentence(IContestDisplay display)
    {
		int option = display.GetLastAnswer();
		contest.AddAnswer(solutions[option]);
		display.SetColorButton(option, solutions[option]? ButtonState.Correct : ButtonState.Fail);
		display.DisableButtons();

		if(solutions[option])
			display.PlayWin();
		else
			display.PlayFail();
			
        return nexts == null? null : nexts[option];
    }

    public void ShowIn(IContestDisplay display)
    {
		display.ShowButtons(answers);
        display.Write(question);
    }

	public bool WaitForButtons()
    {
		return true;
    }

    public void SetNext(IContestSentence next)
    {
		if(nexts == null)
			this.nexts = new IContestSentence[3];
		
        this.nexts[0] = next;
		this.nexts[1] = next;
		this.nexts[2] = next;
    }
}