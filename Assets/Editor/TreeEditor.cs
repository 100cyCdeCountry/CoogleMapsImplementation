using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TreeMaker))]
public class TreeEditor : Editor
{

    public override void OnInspectorGUI()
    {
        TreeMaker treeMaker = (TreeMaker)target;

        base.OnInspectorGUI();
        if(GUILayout.Button("Create Trees")) treeMaker.CreateTrees();

        if(GUILayout.Button("Remove Trees")) treeMaker.RemoveTrees();
        
    }

}
