using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathResult : MonoBehaviour
{

    private int index = 0;
    private SpawnCircle spawnCircle;

    void Start()
    {
        spawnCircle = gameObject.GetComponent<SpawnCircle>();
    }

    public void PreviousPath()
    {
        if (spawnCircle.indexOrder.Count < 9)
            return;
        index--;
        if (index < 0)
        {
            index = 8;
        }
        HidePaths();
        spawnCircle.goPathList[index].gameObject.SetActive(true);
        spawnCircle.offsetGazeList[spawnCircle.indexOrder[index]].gameObject.SetActive(true);
        SpawnCircle.targetCircle[spawnCircle.indexOrder[index]].gameObject.SetActive(true);
    }

    public void NextPath()
    {
        if (spawnCircle.indexOrder.Count < 9)
            return;
        index++;
        if (index > 8)
        {
            index = 0;
        }
        HidePaths();
        spawnCircle.goPathList[index].gameObject.SetActive(true);
        spawnCircle.offsetGazeList[spawnCircle.indexOrder[index]].gameObject.SetActive(true);
        SpawnCircle.targetCircle[spawnCircle.indexOrder[index]].gameObject.SetActive(true);
    }

    private void HidePaths()
    {
        foreach (var go in spawnCircle.goPathList)
        {
            go.gameObject.SetActive(false);
        }
        foreach (var go in SpawnCircle.targetCircle)
        {
            go.gameObject.SetActive(false);
        }
        foreach (var go in spawnCircle.offsetGazeList)
        {
            go.gameObject.SetActive(false);
        }
    }
}
