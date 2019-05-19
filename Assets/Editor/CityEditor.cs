using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CityGenerator))]
public class CityEditor : Editor
{

    public override void OnInspectorGUI()
    {
        CityGenerator cityGenerator = (CityGenerator)target;

        base.OnInspectorGUI();
        if(GUILayout.Button("Create Cities")) cityGenerator.CreateCities();

        if(GUILayout.Button("Remove Cities")) cityGenerator.RemoveCities();
        
    }

}
