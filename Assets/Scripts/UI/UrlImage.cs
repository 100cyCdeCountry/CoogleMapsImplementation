using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UrlImage : MonoBehaviour {

	[SerializeField] string url;
	[SerializeField] private Sprite loadingImage;
    private Image img;

    // Use this for initialization
    void Start () {
		img = GetComponent<Image>();
		if(url != "")
			LoadImageWithoutLoading(url);
		
	}
	
	public void LoadImage(string url){
		if(img == null)
			img = GetComponent<Image>();
		
		img.sprite = loadingImage;
		StartCoroutine(LoadImageCoroutine(url));
	}

	public void LoadImageWithoutLoading(string url){
		StartCoroutine(LoadImageCoroutine(url));
	}

	public IEnumerator LoadImageCoroutine(string url) {
		//warning CS0618: 'WWW' is obsolete: 'Use UnityWebRequest, a fully featured replacement which is more efficient and has additional features'
		WWW www = new WWW(url);
		yield return www;
		img.sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));
	}
	
}
