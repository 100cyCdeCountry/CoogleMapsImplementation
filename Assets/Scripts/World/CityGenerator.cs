using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CityGenerator : MonoBehaviour {

	[System.Serializable]
	public class City{
		public BoxCollider region;
		public float spaceBetween;
		public int centerSpace = 0;
		
		[HideInInspector] public int houses;
		[HideInInspector] public Vector2Int housesDimension;

		public bool IsFull() { 
			return houses >= TotalHouses; 
		}

		public int TotalHouses{
			get{
				return housesDimension.x * housesDimension.y;
			}
		}

		public void CalculateTotalHouses() {
			Bounds bounds = region.bounds;
			housesDimension = Vector2Int.FloorToInt(new Vector2(bounds.size.x, bounds.size.z) / spaceBetween);	
		}

		public void Init() {
			houses = 0;
			CalculateTotalHouses();
		}

		public Vector2Int Get(int i) {
			return new Vector2Int(i % housesDimension.x, i / housesDimension.x);
		}

		public Vector3 GetPosition(Vector2Int cell) {
			return region.bounds.min + new Vector3(cell.x * spaceBetween, 0, cell.y * spaceBetween);
		}

		public Vector3 GetPosition(int i) {
			return GetPosition(Get(i));
		}

	}

	[SerializeField]
	public City[] cities;

	public GameObject[] housesModels;
	
	public int housesInCdeCountry = 2400 / 3;

	private Terrain terrain;

	public void CreateCities () {
		terrain = Terrain.activeTerrain;
		for (int i = 0; i < cities.Length; i++) {
			cities[i].Init();
		}		

		GenerateCities(housesInCdeCountry);

	}

	public void GenerateCities(int amount) {
		int maxHouses = 0;
		List<City> citiesSortByHouseCapacity = new List<City>();
		for (int i = 0; i < cities.Length; i++) {
			maxHouses += cities[i].TotalHouses;
			citiesSortByHouseCapacity.Add(cities[i]);
		}

		citiesSortByHouseCapacity.Sort((a, b) => { return a.TotalHouses - b.TotalHouses; });
		float capacityFactor = (float)amount / (float)maxHouses;

		int totalHouses = 0;
		int remainingHouses = amount;
		for(int i = 0; i < citiesSortByHouseCapacity.Count; i++) {
			int houses = (int)Mathf.Ceil( (float)citiesSortByHouseCapacity[i].TotalHouses * capacityFactor );
			if(remainingHouses - houses < 0)
				houses = remainingHouses;

			remainingHouses -= houses;
			CreateHousesInCity( houses, citiesSortByHouseCapacity[i] );
			totalHouses += houses;
		}
		
	}

	private void CreateHousesInCity(int amount, City city) {
		SpiralIterator it = new SpiralIterator(city.housesDimension.x, city.housesDimension.y);
		for(int i = 0; i < amount; i++) {
			if(!it.Iterate((int p, int x, int y) => {
				if(i < city.centerSpace) return;

				Vector2Int pos = new Vector2Int(x, y);
				CreateHouseInPosition(city.GetPosition(pos));
			})) return;
		}
	}

	private void CreateHouseInPosition(Vector3 position) {
		position.y = terrain.SampleHeight(position);
		float dir = (int)position.sqrMagnitude % 4 * 90;
		
		GameObject house = Instantiate(housesModels[Random.Range(0, housesModels.Length)],
		 position, Quaternion.AngleAxis(dir, Vector3.up));
		house.transform.parent = transform;
		house.tag = "House";
        house.isStatic = true;
	}

	public void RemoveCities() {
        Transform[] allChildren = gameObject.GetComponentsInChildren<Transform>();
		List<GameObject> houses = new List<GameObject>();
        foreach (Transform child in allChildren) {
            if (child.gameObject.tag == "House") {
				houses.Add(child.gameObject);
            }
        }
		foreach (GameObject house in houses) {
			DestroyImmediate(house);
		}
    }

}
