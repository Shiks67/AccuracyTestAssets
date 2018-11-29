using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class SpawnCircle : MonoBehaviour
{

    public GameObject spawnObject;
    public GameObject gazeDot;

    private Vector3[] spawnArea =
    {new Vector3(-30f,30f,-0.05f), new Vector3(0f,30f,-0.05f), new Vector3(30f,30f,-0.05f),
    new Vector3(-30f,0f,-0.05f), new Vector3(0f,0f,-0.05f), new Vector3(30f,0f,-0.05f),
    new Vector3(-30f,-30f,-0.05f), new Vector3(0f,-30f,-0.05f), new Vector3(30f,-30f,-0.05f)};

    public static float[] circleFinalSize = { 30, 30, 30, 30, 30, 30, 30, 30, 30 };
    public static Vector3[] finalGazePos = new Vector3[9];
    private bool[] isVisited = new bool[9];
    public List<int> indexOrder = new List<int>();

    public List<GameObject> goPathList = new List<GameObject>();

    private int index;
    public Text countObj;
    private float countDown = 3f;

    public static List<GameObject> targetCircle = new List<GameObject>();
    public List<GameObject> offsetGazeList = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        countDown -= Time.deltaTime;
        if (countDown > 0)
        {
            countObj.text = System.Math.Round(countDown, 0).ToString();
            return;
        }
        if (countDown < 0)
        {
            countObj.gameObject.SetActive(false);
        }

        if (targetCircle.Count < 1 && targetCircle.Count < 5)
        {
            if (isVisited.Contains(false))
            {
                index = Random.Range(0, 9);
                while (isVisited[index] == true)
                {
                    index = Random.Range(0, 9);
                }
                isVisited[index] = true;
                indexOrder.Add(index);
                newCircle(index);
            }
            else
            {
                Result();
            }
        }
    }

    /// <summary> 
    /// Create a new circle with his id's informations 
    /// </summary>
    /// <param name="id">id of the circle in the array that contain all the positions</param>
    public void newCircle(int id, bool result = false)
    {
        GameObject newObject = Instantiate(spawnObject);
        newObject.transform.SetParent(gameObject.transform);
        newObject.transform.localScale = new Vector3(circleFinalSize[id], 0.1f, circleFinalSize[id]);
        newObject.transform.localRotation = Quaternion.Euler(90, 0, 0);
        newObject.transform.localPosition = spawnArea[id];
        newObject.GetComponent<CircleLife>().Init(id);
        targetCircle.Add(newObject);

        if (result)
        {
            ShowOffset(id, newObject.transform.position);
        }
    }

    private void ShowOffset(int id, Vector3 pos)
    {
        GameObject newGazePoint = Instantiate(gazeDot);
        newGazePoint.transform.SetParent(gameObject.transform);
        newGazePoint.transform.localPosition = finalGazePos[id];

        var go = new GameObject();
        go.transform.SetParent(gameObject.transform);
        var lr = go.AddComponent<LineRenderer>();

        lr.SetPosition(0, new Vector3(pos.x, pos.y, newGazePoint.transform.position.z));
        lr.SetPosition(1, newGazePoint.transform.position);
        lr.startWidth = 0.02f;
        lr.endWidth = 0.02f;
        lr.useWorldSpace = false;

        newGazePoint.transform.SetParent(go.transform);

        offsetGazeList.Add(go);
    }
    private void ShowAllPath()
    {
        foreach (var path in GazeMarker.savedGazePath)
        {
            var go = new GameObject();
            go.transform.SetParent(gameObject.transform.parent);
            go.transform.position = gameObject.transform.localPosition;
            go.transform.localScale = gameObject.transform.localScale;

            var lr = go.AddComponent<LineRenderer>();
            lr.positionCount = path.Count;
            lr.startWidth = 0.02f;
            lr.endWidth = 0.02f;
            lr.useWorldSpace = false;
            lr.material.color = Color.green;

            int positionToSet = 0;
            foreach (var pos in path)
            {
                lr.SetPosition(positionToSet, pos);
                positionToSet++;
            }
            go.transform.SetParent(gameObject.transform);
            goPathList.Add(go);
        }
    }

    /// <summary>
    /// Show every circle with their final size
    /// </summary>
    public void Result()
    {
        DestroyAllCircles();
        for (int i = 0; i < 9; i++)
        {
            newCircle(i, true);
        }
        ShowAllPath();
        WorstOffset();
    }

    private void WorstOffset()
    {
        float worstX = 0;
        float worstY = 0;

        for (int i = 0; i < indexOrder.Count; i++)
        {
            float offsetX = Mathf.Abs(finalGazePos[indexOrder[i]].x);

            float offsetY = Mathf.Abs(finalGazePos[indexOrder[i]].y);

            float circleCenterX = Mathf.Abs(spawnArea[indexOrder[i]].x);

            float circleCenterY = Mathf.Abs(spawnArea[indexOrder[i]].y);

            float offsetDiffX = Mathf.Abs(offsetX - circleCenterX);
            float offsetDiffY = Mathf.Abs(offsetY - circleCenterY);

            if (worstX < offsetDiffX)
                worstX = offsetDiffX;

            if (worstY < offsetDiffY)
                worstY = offsetDiffY;
        }
        FovStatic.horizontalSize = worstX;
        FovStatic.verticalSize = worstY;
    }

    ///<summary> Destroy every GameObject target
    ///<typeparam name="Retry">if true the size and visited positions are reseted</typeparam>
    ///</summary>
    public void DestroyAllCircles(bool retry = false)
    {
        foreach (var obj in targetCircle)
        {
            Destroy(obj);
        }
        foreach (var obj in offsetGazeList)
        {
            Destroy(obj);
        }

        if (retry)
        {
            SpawnCircle.targetCircle.Clear();
            offsetGazeList.Clear();

            circleFinalSize = new float[] { 30, 30, 30, 30, 30, 30, 30, 30, 30 };
            SpawnCircle.finalGazePos = new Vector3[9];
            GazeMarker.gazePath.Clear();
            GazeMarker.savedGazePath.Clear();
            isVisited = new bool[9];
            countObj.gameObject.SetActive(true);
            countDown = 3f;
        }
    }
}
