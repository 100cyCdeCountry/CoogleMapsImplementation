using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

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
		
		img.overrideSprite = loadingImage;
		StartCoroutine(LoadImageCoroutine(url));
	}

	public void LoadImageWithoutLoading(string url){
		StartCoroutine(LoadImageCoroutine(url));
	}

	public IEnumerator LoadImageCoroutine(string url) {
		//warning CS0618: 'WWW' is obsolete: 'Use UnityWebRequest, a fully featured replacement which is more efficient and has additional features'
		UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
		yield return www.SendWebRequest();

		if(www.isNetworkError || www.isHttpError) {
			Debug.Log(www.error);
		}
		else {
			Texture2D texture = (Texture2D)((DownloadHandlerTexture)www.downloadHandler).texture;
			Sprite s = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0), 100, 0, SpriteMeshType.FullRect);

			img.overrideSprite = s;

		}
		
	}

	public void ReleaseImage() {
		img.overrideSprite = null;
	}
	
}
