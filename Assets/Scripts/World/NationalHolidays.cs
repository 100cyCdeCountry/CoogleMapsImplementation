using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class NationalHolidays : MonoBehaviour {

    public GameObject fireworks;
    public GameObject tag;
    public Transform gameObjectWithTags;

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

    [System.Serializable]
    public struct NationalHoliday{
        public int day;
        public Month month;
        public string name;
        public Transform place;

        public bool isToday() {
            var date = DateTime.Now.ToLocalTime();
            return date.Day == day && date.Month == ((int)month + 1);
        }

        [SerializeField] public UnityEvent callback;

    }
	
    public List<NationalHoliday> nationalHolidays;

	void Start () {
        var holidaysHappeningToday = nationalHolidays.Where(h => h.isToday());

        foreach(var holiday in holidaysHappeningToday) {
            holiday.callback.Invoke();
            CreateTagFor(holiday);
        }
        
	}

    public void ShotFireworks(Transform place) {
        GameObject.Instantiate(fireworks, place.position, fireworks.transform.rotation);
    }

    public void CreateTagFor(NationalHoliday holiday) {
        var partyTag = GameObject.Instantiate(tag, gameObjectWithTags);
        partyTag.name = "PARTY: " + holiday.name;
        partyTag.transform.position = holiday.place.position;
        var geoTag = partyTag.GetComponent<GeoTag>();
        geoTag.tagName = holiday.name;
        geoTag.type = GeoTag.Type.Holiday;
        
    }

}
