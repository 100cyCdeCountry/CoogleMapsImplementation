using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandHeld : MonoBehaviour {

	public float size = 2;
	public float velocity = 2;
	Vector3 initialPosition;

	// Use this for initialization
	void Start () {
		initialPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 position = initialPosition;
		position.x += Mathf.PerlinNoise(Time.time * velocity, 0) * size;
		position.y += Mathf.PerlinNoise(0, Time.time * velocity) * size;
		transform.position = position;
		
	}
}
