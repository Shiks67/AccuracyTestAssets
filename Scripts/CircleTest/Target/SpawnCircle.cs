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

    private int index;
    public Text countObj;
    private float countDown = 3f;

    public static List<GameObject> targetCircle = new List<GameObject>();
    private List<GameObject> gazeList = new List<GameObject>();

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
    private void newCircle(int id, bool result = false)
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
            GameObject newGazePoint = Instantiate(gazeDot);
            newGazePoint.transform.SetParent(gameObject.transform);
            newGazePoint.transform.localPosition = finalGazePos[id];
            gazeList.Add(newGazePoint);

            var go = new GameObject();
            go.transform.SetParent(gameObject.transform);
            var lr = go.AddComponent<LineRenderer>();

            lr.SetPosition(0, new Vector3(newObject.transform.position.x, newObject.transform.position.y, newGazePoint.transform.position.z));
            lr.SetPosition(1, newGazePoint.transform.position);
            lr.startWidth = 0.04f;
            lr.endWidth = 0.04f;
            lr.useWorldSpace = false;
        }
    }

    private void ShowPath()
    {
        foreach (var path in GazeMarker.oldGazePath)
        {
            var go = new GameObject();
            go.transform.SetParent(gameObject.transform.parent);
            go.transform.position = gameObject.transform.localPosition;
            go.transform.localScale = gameObject.transform.localScale;

            var lr = go.AddComponent<LineRenderer>();
            lr.positionCount = path.Count;
            lr.startWidth = 0.04f;
            lr.endWidth = 0.04f;
            lr.useWorldSpace = false;
            lr.material.color = Color.green;

            int positionToSet = 0;
            foreach (var pos in path)
            {
                lr.SetPosition(positionToSet, pos);
                positionToSet++;
            }
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
        ShowPath();
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
        foreach (var obj in gazeList)
        {
            Destroy(obj);
        }
        if (!retry)
            newCircle(index);

        if (retry)
        {
            SpawnCircle.targetCircle.Clear();
            gazeList.Clear();

            circleFinalSize = new float[] { 30, 30, 30, 30, 30, 30, 30, 30, 30 };
            SpawnCircle.finalGazePos = new Vector3[9];
            GazeMarker.gazePath.Clear();
            GazeMarker.oldGazePath.Clear();
            isVisited = new bool[9];
            countObj.gameObject.SetActive(true);
            countDown = 3f;
        }
    }
}
