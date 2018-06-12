using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TreeMaker : MonoBehaviour {

	[SerializeField] int amount = 500;
	[SerializeField] GameObject[] treePrefabs;
	[SerializeField] Transform water;
    [SerializeField] private float maxHeight = 70;
    [SerializeField] private float minHeight = 10;

    CityGenerator cities;

    public BoxCollider[] noTreesRegions;

    // Use this for initialization
    void Start () {
        cities = GetComponent<CityGenerator>();
		Terrain terrain = Terrain.activeTerrain;
		for(int i = 0; i < amount; i++)
        {
            Vector3 randomPos = GetRandomSurfacePosition(terrain);

            CreateTree(randomPos);
        }
    }

    private Vector3 GetRandomSurfacePosition(Terrain terrain)
    {
        Vector3 randomPos;
        Vector3 terrainDimension = terrain.terrainData.size;
        do
        {
            randomPos = new Vector3(Random.Range(0, terrainDimension.x),
                0, Random.Range(0, terrainDimension.z));
            randomPos.y = terrain.SampleHeight(randomPos);
        } while (!CouldTreeSpawnIn(randomPos));
        return randomPos;
    }

    private bool CouldTreeSpawnIn(Vector3 randomPos)
    {
        return randomPos.y > water.position.y &&
         randomPos.y < maxHeight &&
         randomPos.y > SurfaceManager.waterHeight + minHeight &&
          !cities.cities.Any((CityGenerator.City city) =>
          {
              Vector3 pos = randomPos;
              pos.y = city.region.bounds.center.y;
              return city.region.bounds.Contains(pos);
          }) &&
          !noTreesRegions.Any((BoxCollider box) =>
          {
              Vector3 pos = randomPos;
              pos.y = box.bounds.center.y;
              return box.bounds.Contains(pos);
          });
    }

    private void CreateTree(Vector3 position)
    {
        Instantiate(treePrefabs[Random.Range(0, treePrefabs.Length)],
                        position, Quaternion.Euler(0, Random.Range(0, 360), 0));
    }
}
