using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnClick : MonoBehaviour {

	public GameObject other;

	void OnMouseDown()
	{
		if(other != null)
			Destroy(other);
	}

}
