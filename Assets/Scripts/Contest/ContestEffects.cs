using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class EffectSentence : CanGetNext, IContestSentence 
{
	public IContestSentence next;
	public string effectName;

	private ContestEffects effects;

	public EffectSentence(string name, IContestSentence next = null)
	{
		this.effectName = name;
		this.next = next;
	}

	public IContestSentence GoToNextSentence(IContestDisplay display)
	{
		return next;
	}

	public void SetNext(IContestSentence next)
	{
		this.next = next;
	}

	public void ShowIn(IContestDisplay display)
	{
		effects = display.GetCharacter().GetComponent<ContestEffects>();
		ContestEffects.Action action = effects.Get(effectName);
		switch (effects.Get(effectName).mode)
		{
			case ContestEffects.Action.Mode.PlayAnimation:
			case ContestEffects.Action.Mode.PlayAnimationUntilEnd:
				var anim = effects.GetComponent<Animation>();
				if(action.parameter != "") 
					anim.clip = anim.GetClip(action.parameter);
				anim.Play();
			break;
			case ContestEffects.Action.Mode.StopAnimation:
				effects.GetComponent<Animation>().Stop();
			break;
			case ContestEffects.Action.Mode.PlaySound:
				var audio = effects.GetComponent<AudioSource>();
				if(action.parameter != "") 
					audio.clip = effects.GetSound(action.parameter);
				audio.Play();
			break;
			case ContestEffects.Action.Mode.StopSound:
				effects.GetComponent<Animation>().Stop();
			break;
			case ContestEffects.Action.Mode.PlayParticules:
				effects.GetComponent<ParticleSystem>().Play();
			break;
			case ContestEffects.Action.Mode.StopParticules:
				effects.GetComponent<ParticleSystem>().Stop();
			break;
			case ContestEffects.Action.Mode.ChangeSprite:
				effects.GetComponent<SpriteRenderer>().sprite = effects.GetSprite(action.parameter);
			break;
		}

		if(action.autoGoNext)
			display.MoveNext();
	}

	public bool WaitForButtons()
	{
		if(effects.Get(effectName).mode == ContestEffects.Action.Mode.PlayAnimationUntilEnd)
			return effects.GetComponent<Animation>().isPlaying;
		else
			return false;
	}

	public override IContestSentence Next()
	{
		return next;
	}

}

public class ContestEffects : MonoBehaviour {

	[System.Serializable]
	public class Action {
		public string name;
		public enum Mode{
			PlayAnimation,
			PlayAnimationUntilEnd,
			StopAnimation,
			PlayParticules,
			StopParticules,
			PlaySound,
			StopSound,
			ChangeSprite
		}
		public Mode mode;
		public string parameter;
		public bool autoGoNext = true;
	}

	[SerializeField]
    private List<Action> events;

	public Action Get(string name) {
		return events.Find((Action a) => a.name == name);
	}

	public Sprite GetSprite(string name) {
		return imagesToSwitch.Find((ImagePair i) => i.name == name).sprite;
	}

	public AudioClip GetSound(string name) {
		return soundsToPlay.Find((SoundPair s) => s.name == name).sound;
	}

	[System.Serializable]
	struct ImagePair{
		public string name;
		public Sprite sprite;
	}

	[SerializeField]
    private List<ImagePair> imagesToSwitch;

	[System.Serializable]
	struct SoundPair{
		public string name;
		public AudioClip sound;
	}

	[SerializeField]
    private List<SoundPair> soundsToPlay;

}
