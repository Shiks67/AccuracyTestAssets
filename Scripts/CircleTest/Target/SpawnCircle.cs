using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class SpawnCircle : MonoBehaviour
{

    public GameObject spawnObject;
    private Vector3[] spawnArea =
    {new Vector3(-30f,30f,-0.05f), new Vector3(0f,30f,-0.05f), new Vector3(30f,30f,-0.05f),
    new Vector3(-30f,0f,-0.05f), new Vector3(0f,0f,-0.05f), new Vector3(30f,0f,-0.05f),
    new Vector3(-30f,-30f,-0.05f), new Vector3(0f,-30f,-0.05f), new Vector3(30f,-30f,-0.05f)};

    public static float[] circleFinalSize = { 30, 30, 30, 30, 30, 30, 30, 30, 30 };
    public static Vector3[] finalGazePos = new Vector3[9];
    private bool[] isVisited = new bool[9];

    private int index;
    private GameObject canvasParent;
    public Text countObj;
    private float countDown = 3f;

    public static List<GameObject> targetCircle = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        canvasParent = GameObject.Find("Quadri");
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
    private void newCircle(int id)
    {
        GameObject newObject = Instantiate(spawnObject);
        newObject.transform.SetParent(canvasParent.transform);
        newObject.transform.localScale = new Vector3(circleFinalSize[id], 0.1f, circleFinalSize[id]);
        newObject.transform.localRotation = Quaternion.Euler(90, 0, 0);
        newObject.transform.localPosition = spawnArea[id];
        newObject.GetComponent<CircleLife>().Init(id);
        targetCircle.Add(newObject);
    }

    /// <summary>
    /// Show every circle with their final size
    /// </summary>
    public void Result()
    {
        DestroyAllCircles();
        for (int i = 0; i < 9; i++)
        {
            newCircle(i);
        }
    }

    ///<summary> Destroy every GameObject target
    ///<typeparam name="Retry">if true the size and visited positions are reseted</typeparam>
    ///</summary>
    public void DestroyAllCircles(bool retry = false)
    {
        if (retry)
        {
            // foreach (var obj in GazeMarker.listDotMark)
            // {
            //     Destroy(obj);
            // }
            // foreach (var objList in GazeMarker.oldListDotMark)
            // {
            //     foreach (var obj in objList)
            //     { Destroy(obj); }
            // }
            circleFinalSize = new float[] { 30, 30, 30, 30, 30, 30, 30, 30, 30 };
            SpawnCircle.finalGazePos = new Vector3[9];
            GazeMarker.listDotMark.Clear();
            GazeMarker.oldListDotMark.Clear();
            isVisited = new bool[9];
            countObj.gameObject.SetActive(true);
            countDown = 3f;
        }
        foreach (var obj in targetCircle)
        {
            Destroy(obj);
        }
        SpawnCircle.targetCircle.Clear();
    }
}
