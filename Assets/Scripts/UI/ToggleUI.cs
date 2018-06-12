using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ToggleUI : MonoBehaviour, IPointerClickHandler {

	[SerializeField] private bool isOn;
	[SerializeField] private Sprite onSprite;
	[SerializeField] private Sprite offSprite;

	[System.Serializable]
 	public class ChangeEvent : UnityEvent<bool>{}
	[SerializeField] public ChangeEvent OnChange;

	private Image image;

	void Start() {
		image = GetComponent<Image>();
		
	}

	public bool Active{
		get{
			return isOn;
		}

		set{
			isOn = value;

			image.sprite = value? onSprite : offSprite;
			if(OnChange != null) {
				OnChange.Invoke(value);
			}
		}
	}

	public void OnPointerClick(PointerEventData eventData) // 3
     {
		 Active = !Active;
     }

}
