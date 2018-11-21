﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DrawGazePath : MonoBehaviour
{

    // Use this for initialization
    private LineRenderer line;
    private Vector3 lastPos;
    private List<Vector3> currentLinePoints = new List<Vector3>();
    void Start()
    {

        SetupLine();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            GazeMarker.oldGazePath.Add(new List<Vector3>(currentLinePoints));
            currentLinePoints.Clear();
            line.positionCount = 0;
        }
        if (lastPos != GazeMarker.gazePath.Last() && SpawnCircle.targetCircle.Count == 1)
        {
            lastPos = GazeMarker.gazePath.Last();
            AddLinePoint(lastPos);
        }
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
        line.SetPosition(0, GazeMarker.gazePath.Last());
    }

    void AddLinePoint(Vector3 lastPos)
    {
        currentLinePoints.Add(lastPos); // add the new point to our saved list of line points
        line.positionCount = currentLinePoints.Count; // set the line’s vertex count to how many points we now have, which will be 1 more than it is currently
        line.SetPosition(currentLinePoints.Count - 1, lastPos); // add newPoint as the last point on the line (count -1 because the SetPosition is 0-based and Count is 1-based)    
    }
}