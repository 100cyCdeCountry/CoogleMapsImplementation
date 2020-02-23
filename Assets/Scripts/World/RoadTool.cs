using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter))]
public class RoadTool : MonoBehaviour
{

    public List<Vector3> points = new List<Vector3>();
    public bool showPath = false;
    public float upOffset = 0;
    public float pathWidth = 1;
    public float uvsY = 1;

    private int selected = -1;
    private bool dragged = false;

    private const float selectThreshold = 1;
    private const float lineThreshold = 2;

    private List<Vector3> debugPoints = new List<Vector3>();

#if UNITY_EDITOR

    void OnDrawGizmos() {
        
        if (showPath && Event.current.type == EventType.Repaint)
        {
            DrawPath();
        }

    }

    private void DrawPath() {
        Gizmos.color = Color.red;
        Handles.color = Color.blue;
        
        for(int i = 0; i < points.Count; i++) {
            if(i > 0) {
                Gizmos.DrawLine(transform.TransformPoint(points[i - 1]) + Vector3.up * (upOffset + 1),
                                transform.TransformPoint(points[i]) + Vector3.up * (upOffset + 1));
            }
            
            if(i == selected) {
                Handles.color = Color.yellow;
            }
            Handles.DotHandleCap(0, transform.TransformPoint(points[i]),
                                Quaternion.identity, 1, EventType.Repaint);
            if(i == selected) {
                Handles.color = Color.blue;
            }
        }

        Handles.color = Color.cyan;
        for(int i = 0; i < debugPoints.Count; i++) {
            Handles.DotHandleCap(0, debugPoints[i],
                                Quaternion.identity, 1, EventType.Repaint);
        }
    }
    
#endif

    public Vector3[] GetPath() {
        Vector3[] pointsInWorld = new Vector3[points.Count];
        for(int i = 0; i < points.Count; i++) {
            pointsInWorld[i] = transform.TransformPoint(points[i]);
        }
        
        return pointsInWorld;
    }

    public void ClearPath(){
        points.Clear();
        GetComponent<MeshFilter>().mesh = new Mesh();
        debugPoints.Clear();
    }

    public void AddPoint(Vector3 position) {
        points.Add(transform.InverseTransformPoint(position));
        CreateMesh();
    }

    public void InsertPoint(Vector3 position, int index) {
        points.Insert(index, transform.InverseTransformPoint(position));
        CreateMesh();
    }

    public void SelectPoint(int index) {
        selected = index;
    }

#if UNITY_EDITOR
    public void ManageClick(Vector2 mousePos){
        Ray ray = HandleUtility.GUIPointToWorldRay(mousePos);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, Mathf.Infinity)){
            int posiblePoint;
            if(isNearPoint(hit.point, out posiblePoint)) {
                
                SelectPoint(posiblePoint);
                return;
            }

            if(IsBetweenPoints(hit.point, out posiblePoint)) {
                posiblePoint++;
                InsertPoint(hit.point, posiblePoint);
                SelectPoint(posiblePoint);
                return;
            }

            AddPoint(hit.point);

        }
        
    }
    
#endif

    private bool IsBetweenPoints(Vector3 point, out int index){
        for(int i = 0; i < points.Count -1; i++) {
            Vector3 start = transform.TransformPoint(points[i]);
            Vector3 direction = transform.TransformVector(points[i + 1] - points[i]);
                                
            Vector3 nearestPoint;
            if(!NearestPointOnLine(start, direction, point, out nearestPoint)) 
                continue;

            float distance = Vector3.Distance(nearestPoint, point);
            

            if(distance < lineThreshold) {
                index = i;
                return true;
            }
        }
        index = -1;
        return false;
    }

    public bool NearestPointOnLine(Vector3 linePnt, Vector3 lineDir, Vector3 pnt, out Vector3 nearest)
    {
        var dir = lineDir.normalized;
        var v = pnt - linePnt;
        var d = Vector3.Dot(v, dir);
        nearest = linePnt + dir * d;
        return d >= 0 && d <= lineDir.magnitude;/*new Bounds(linePnt + lineDir/2.0f, lineDir).Contains(nearest);*/
    }

#if UNITY_EDITOR

    public void ManageDrag(Vector2 mousePosition)
    {
        if(selected == -1) return;

        Ray ray = HandleUtility.GUIPointToWorldRay(mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, Mathf.Infinity)){
            dragged = true;
            points[selected] = transform.InverseTransformPoint(hit.point);
            CreateMesh();
        }

    }

        
#endif

    private void RemovePoint(int indexToRemove)
    {
        points.RemoveAt(indexToRemove);
        CreateMesh();
    }

    public void RemoveLast()
    {
        if(points.Count > 0)
            points.RemoveAt(points.Count -1);

        CreateMesh();
    }

#if UNITY_EDITOR

    public void ManageMouseUp(Vector2 mousePosition)
    {
        if(!dragged && selected != -1)
            RemovePoint(selected);

        selected = -1;
        dragged = false;

    }

        
#endif

    private bool isNearPoint(Vector3 point, out int posiblePoint)
    {
        float threshold = 1;
        for(int i = 0; i < points.Count; i++){
            if(Vector3.Distance(transform.TransformPoint(points[i]), point) < threshold) {
                posiblePoint = i;
                return true;
            }
        }
        posiblePoint = -1;
        return false;
    }

    public void CreateMesh() {

        if(points.Count < 2) {
            GetComponent<MeshFilter>().mesh = new Mesh();  
            return;
        }

        Mesh mesh = new Mesh(); 

        Vector3[] vertices = new Vector3[points.Count * 2];
        Vector3[] normals = new Vector3[points.Count * 2];
        int[] triangles = new int[(points.Count - 1) * 2 * 3];
        Vector2[] uvs = new Vector2[points.Count * 2];

        float distance = 0;

        for(int i = 0; i < points.Count; i++) {

            Vector3 forward = Vector3.zero;
            Vector3 back = Vector3.zero;


            if(i != 0) 
                back = points[i] - points[i - 1];
            
            if(i != points.Count - 1) 
                forward = points[i] - points[i + 1];
            
            Vector3 midForwad = (forward.normalized - back.normalized).normalized;

            Vector3 widthOffset = new Vector3(-midForwad.z, 0, midForwad.x);

            vertices[i * 2] = points[i] 
                + widthOffset * pathWidth
                + Vector3.up * upOffset;
            vertices[i * 2 + 1] = points[i] 
                + -widthOffset * pathWidth
                + Vector3.up * upOffset;

            normals[i * 2] = Vector3.up;
            normals[i * 2 + 1] = Vector3.up;

            uvs[i * 2] = new Vector2(0, distance * uvsY);
            uvs[i * 2 + 1] = new Vector2(1, distance * uvsY);

            distance += forward.magnitude;

        }

        for(int i = 0; i < points.Count - 1; i++) {
            int p0 = i * 2;
            int p1 = i * 2 + 1;
            int p2 = i * 2 + 2;
            int p3 = i * 2 + 3;

            // First triangle
            triangles[i * 6 + 0] = p0;
            triangles[i * 6 + 1] = p1;
            triangles[i * 6 + 2] = p3;

            // Second triangle
            triangles[i * 6 + 3] = p0;
            triangles[i * 6 + 4] = p3;
            triangles[i * 6 + 5] = p2;
        }

        mesh.name = "Roads";
        mesh.vertices = vertices; 
        mesh.triangles = triangles;
        mesh.normals = normals;
        mesh.uv = uvs;

        GetComponent<MeshFilter>().mesh = mesh; 

    }

    
}
