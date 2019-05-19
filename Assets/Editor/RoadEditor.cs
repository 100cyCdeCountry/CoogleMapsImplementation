using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using UnityEditor;
using UnityEngine.UIElements;

[CustomEditor(typeof(RoadTool))]
public class LevelScriptEditor : Editor 
{

    private bool editMode = false; 
    private int hashCode;
    int controlID;

    RoadTool roadTool;    

    void Start()
    {
        hashCode = GetHashCode();
        
        controlID = GUIUtility.GetControlID(hashCode, FocusType.Passive);
    }


     
    public override void OnInspectorGUI()
    {
        roadTool = (RoadTool)target;

        EditorGUILayout.LabelField("Points", roadTool.points.Count.ToString());
        if(GUILayout.Button("Remove Points")) ClearPath();
        if(GUILayout.Button("Remove Last Point")) RemoveLastPoint();

        if(GUILayout.Button(!editMode? "Edit Path" : "Exit edit path mode")) ToggleEditMode();

        float upOffset = EditorGUILayout.FloatField("Up offset", roadTool.upOffset);
        float pathWidth = EditorGUILayout.FloatField("Path Width", roadTool.pathWidth);
        float uvs = EditorGUILayout.FloatField("Uv Length", roadTool.uvsY);

        if(upOffset != roadTool.upOffset 
            || pathWidth != roadTool.pathWidth
            || uvs != roadTool.uvsY) {
            roadTool.upOffset = upOffset;
            roadTool.pathWidth = pathWidth;
            roadTool.uvsY = uvs;
            roadTool.CreateMesh();
        }

    }

    public void OnSceneGUI()
    {
        roadTool = (RoadTool)target;

        Event e = Event.current;
        
        if (editMode && e.button == 0)
        {
            bool used = false;

            if(e.type == EventType.MouseDown) {
                roadTool.ManageClick(e.mousePosition);
                used = true;
            }

            if(e.type == EventType.MouseDrag) {
                roadTool.ManageDrag(e.mousePosition);
                used = true;
            }
            
            if(e.type == EventType.MouseUp) {
                roadTool.ManageMouseUp(e.mousePosition);
                used = true;
            }

            if(used) {
                e.Use();  
                SceneView.RepaintAll();
            }

        }


        if (e.type == EventType.Layout && editMode) {
            HandleUtility.AddDefaultControl(controlID);
        }

    }

    private void ClearPath() {
        roadTool.ClearPath();
        SceneView.RepaintAll();
    }

    private void ToggleEditMode(){
        editMode = !editMode;
        roadTool.showPath = editMode;
        HandleUtility.AddDefaultControl(editMode? controlID : 0);

    }

    private void RemoveLastPoint() {
        roadTool.RemoveLast();
        SceneView.RepaintAll();
    }

}