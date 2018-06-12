using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeoTag : MonoBehaviour {

	public string tagName;
	public enum Type
	{
		City,
		Sea,
		River,
		Mount,	
		Bay,
	}

	public Type type;

	private struct TypeData{
		public float scale;
		public float maxHeight;
		public Color color;
	};

	private static Dictionary<Type, TypeData> typeDatas;

	private float height = 20;
	private float surfaceHeight;
	private Text text;

	// Use this for initialization
	void Start () {
		if(typeDatas == null)
			InitTypeData();

		text = GetComponentInChildren<Text>();
		text.text = tagName;
		text.color = typeDatas[type].color;

		Vector3 position = transform.position;
		surfaceHeight = SurfaceManager.GetSurfaceHeight(position);
		position.y = surfaceHeight + height;
		transform.position = position;
		
	}
	
	// Update is called once per frame
	void Update () {

		if(Camera.main == null) return;

		TypeData data = typeDatas[type];
		float cameraHeight = Camera.main.transform.position.y;

		Color c = text.color;
		c.a = Mathf.Lerp(0, 1, (surfaceHeight + data.maxHeight - cameraHeight) * 0.08f);
		text.color = c;

		float desiredScale = (Camera.main.transform.position.y - surfaceHeight - height) * data.scale;
		transform.localScale = Vector3.one * Mathf.Min(desiredScale, 3.0f * data.scale / 0.02f);
	}

	static void InitTypeData() {
		typeDatas = new Dictionary<Type, TypeData>();
		typeDatas.Add(Type.City, new TypeData{scale = 0.02f, maxHeight = 1000, color = Color.white});
		typeDatas.Add(Type.Mount, new TypeData{scale = 0.018f, maxHeight = 250, color = Color.green});
		typeDatas.Add(Type.River, new TypeData{scale = 0.015f, maxHeight = 200, color = Color.cyan});
		typeDatas.Add(Type.Sea, new TypeData{scale = 0.02f, maxHeight = 1000, color = Color.blue});
		typeDatas.Add(Type.Bay, new TypeData{scale = 0.015f, maxHeight = 350, color = new Color(0.25f, 0.25f, 1, 1)});
	}

}
