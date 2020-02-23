using UnityEngine;
using System.Collections;
using System;

public class SurfaceManager : MonoBehaviour{

    public GameObject sea;
    public Terrain leftTerrain;
    public Terrain rightTerrain;

    public Terrain riverTerrains;

    private static float terrainXSeparation = 1024;

    private static SurfaceManager singleton;

    void Awake() {
        singleton = this;
    }

    public static SurfaceManager Singleton() {
        return singleton;
    }

    public static float GetSurfaceHeight(Vector3 position, float height = 0) {
        return GetSurfaceHeight(Singleton(), position, height);
    }

    public static float GetSurfaceHeight(SurfaceManager manager, Vector3 position, float height = 0) {
        Terrain terrain = position.x < terrainXSeparation? manager.leftTerrain : manager.rightTerrain;
        float waterHeight = WaterHeight(manager);
        return Mathf.Max( waterHeight, height + terrain.SampleHeight(position) );
    }

    public static float WaterHeight() {
        return WaterHeight(Singleton());
    }

    public static float WaterHeight(SurfaceManager manager) {
        return manager.sea.transform.position.y;
    }

    public static Vector3 SurfaceDimension() {
        return new Vector3(2048, 0, 1024); // Vivan los magic numbers
    }

    public static Vector3 SurfacePosition() {
        return new Vector3(0, 0, 1024);
    }

    public static bool IsWater(SurfaceManager surface, Vector3 position) {
        float terrainHeight = GetSurfaceHeight(surface, position);
        if(WaterHeight(surface) == terrainHeight)
            return true;
        return surface.riverTerrains.SampleHeight(position) > terrainHeight;
    }

}
