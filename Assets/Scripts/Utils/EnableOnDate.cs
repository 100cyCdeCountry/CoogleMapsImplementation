using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnableOnDate : MonoBehaviour {

	public int day;
	public Month month;

	public enum Month{
		January = 0,
		February = 1,
		March = 2,
		April = 3,
		May = 4,
		June = 5,
		July = 6,
		August = 7,
		September = 8,
		October = 9,
		November = 10,
		December = 11
	};


	void Start () {
		var date = DateTime.Now.ToLocalTime();
		bool isDate = date.Day == day && date.Month == ((int)month + 1);

		if(isDate) {
			for (int childId = 0; childId < gameObject.transform.childCount; childId++)
			{
				gameObject.transform.GetChild(childId).gameObject.SetActive(true);	
			}
		}
			
	}
	
}
