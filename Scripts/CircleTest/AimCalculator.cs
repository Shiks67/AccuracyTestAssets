using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimCalculator : MonoBehaviour
{

    private GameObject[] lastDots;
    
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GazeMarker.listDotMark.Count > 1)
            return;

        for (int i = 0; i < 5; i++)
        {
            lastDots[i] = GazeMarker.listDotMark
			[(GazeMarker.listDotMark.Count - 5) + i];
        }
    }
}
