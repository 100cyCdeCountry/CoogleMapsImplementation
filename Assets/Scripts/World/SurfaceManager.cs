using UnityEngine;
using System.Collections;
using System;

class SurfaceManager{

    public static float waterHeight = 50.1f;

    public static float GetSurfaceHeight(Vector3 position, float height = 0) {
        return Mathf.Max( waterHeight, height + Terrain.activeTerrain.SampleHeight(position) );
    }

}