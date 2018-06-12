using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine;

public class OnClick : MonoBehaviour, IPointerClickHandler {
	
	[SerializeField] public UnityEvent ClickEvent;

	public void OnPointerClick(PointerEventData eventData) 
    {
		OnMouseDown();
    }
	
	void OnMouseDown()
	{
		if(ClickEvent != null) {
			ClickEvent.Invoke();
		}
    }
}
