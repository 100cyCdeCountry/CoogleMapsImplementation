using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCars : MonoBehaviour
{
    
    public GameObject carPrefab;

    [System.Serializable]
    public struct RoadStats{
        public RoadTool road;
        public int carAmount;
    }
    public RoadStats[] roads;
    public Color[] colors;

    void Start()
    {
        foreach (var road in roads)
        {
            for(int i = 0; i < road.carAmount; i++) {
                GameObject car = Instantiate(carPrefab, Vector3.zero, Quaternion.identity);
                Vehicle vehicle = car.GetComponent<Vehicle>();
                vehicle.roadTool = road.road;
                vehicle.forward = Random.Range(0.0f, 1.0f) > .5;
                vehicle.startNode = Random.Range(0, road.road.points.Count);
                car.GetComponent<MeshRenderer>().material.color = colors[Random.Range(0, colors.Length)];
            }
        }
    }

}
