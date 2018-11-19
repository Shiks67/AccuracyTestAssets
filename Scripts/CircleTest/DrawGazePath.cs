using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawGazePath : MonoBehaviour
{

    // Use this for initialization
    public GameObject GazeMarker;
    private LineRenderer line;
    private List<Vector3> storedLinePoints = new List<Vector3>();
    void Start()
    {
        line = gameObject.AddComponent<LineRenderer>();
        SetupLine();
    }

    // Update is called once per frame
    void Update()
    {
        AddLinePoint(GazeMarker.transform.localPosition);
    }

    void SetupLine()
    {
        line.sortingLayerName = "OnTop";
        line.sortingOrder = 5;
        line.SetPosition(0, GazeMarker.transform.localPosition);
        line.SetWidth(0.5f, 0.5f);
        line.useWorldSpace = true;
        line.material.color = Color.black;
    }

    void AddLinePoint(Vector3 newPoint)
    {
        storedLinePoints.Add(newPoint); // add the new point to our saved list of line points
        line.SetVertexCount(storedLinePoints.Count); // set the line’s vertex count to how many points we now have, which will be 1 more than it is currently
        line.SetPosition(storedLinePoints.Count - 1, newPoint); // add newPoint as the last point on the line (count -1 because the SetPosition is 0-based and Count is 1-based)    
    }
}
