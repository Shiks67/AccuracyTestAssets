using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DrawGazePath : MonoBehaviour
{

    // Use this for initialization
    public GameObject gazePosition;
    private LineRenderer line;
    private List<Vector3> storedLinePoints = new List<Vector3>();
    void Start()
    {
        
        SetupLine();
    }

    // Update is called once per frame
    void Update()
    {
        // AddLinePoint(gazePosition.transform.localPosition);

        AddLinePoint();
    }

    void SetupLine()
    {
        line = gameObject.AddComponent<LineRenderer>();
        line.material.color = Color.red;
        line.startWidth = 0.04f;
        line.endWidth = 0.04f;
        line.useWorldSpace = false;
        line.sortingLayerName = "UIdata";
        line.sortingOrder = 5;
        line.SetPosition(0, GazeMarker.listDotMark.Last().gameObject.transform.localPosition);
    }

    void AddLinePoint()
    {
        storedLinePoints.Add(GazeMarker.listDotMark.Last().gameObject.transform.localPosition); // add the new point to our saved list of line points
        line.SetVertexCount(GazeMarker.listDotMark.Count); // set the line’s vertex count to how many points we now have, which will be 1 more than it is currently
        line.SetPosition(GazeMarker.listDotMark.Count - 1, GazeMarker.listDotMark.Last().gameObject.transform.localPosition); // add newPoint as the last point on the line (count -1 because the SetPosition is 0-based and Count is 1-based)    
    }
}
