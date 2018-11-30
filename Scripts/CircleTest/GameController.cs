using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{

    private RayCaster rCaster;
    private RaycastHit hit;
    private RaycastHit[] hits;
    public GameObject gazePosObj;
    public static Vector3 gazePosition;

    // private GameObject mainCamera;
    // private bool dotMarkVisibility;
    public GameObject menu;

    // Use this for initialization
    void Start()
    {
        // rCaster = GameObject.FindGameObjectWithTag("EditorOnly").GetComponent<RayCastF>();
        rCaster = Camera.main.GetComponent<RayCaster>();
        LoggerBehavior.sceneName = "CircleTest";
        LoggerBehavior.sceneTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        LoggerBehavior.sceneTimer += Time.deltaTime;
        GazePosUpdate();
        if (Input.GetKeyUp(KeyCode.M))
            menu.SetActive(!menu.activeSelf);

        if (Physics.Raycast(rCaster.ray, out hit))
        {

            //if there is max 1 circle on the grid
            if (SpawnCircle.targetCircle.Count == 1)
            {
                if (SpawnCircle.targetCircle.Contains(hit.transform.gameObject))
                {
                    ReduceCircle(hit.transform.gameObject);
                }
                else
                {
                    ExtendCircle(SpawnCircle.targetCircle.First());
                }
            }
        }
    }

    void GazePosUpdate()
    {
        hits = Physics.RaycastAll(rCaster.ray);
        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            if (hit.transform.name == "Quadri")
            {
                gazePosObj.transform.localPosition = hit.transform.InverseTransformPoint(hit.point);
                gazePosition = hit.transform.InverseTransformPoint(hit.point);
                // print(gazePosObj.transform.localPosition);
            }
        }
    }

    /// <summary>
    /// Reduce the size of the circle
    /// </summary>
    /// <param name="circle">GameObject of the current circle</param>
    private void ReduceCircle(GameObject circle)
    {
        //10f * Time.deltaTime so the computers speed doesn't affect the speed
        if (circle.transform.localScale.x > 0)
        {
            circle.transform.localScale =
           new Vector3(circle.transform.localScale.x - 15f * (Time.deltaTime * 4),
           0.1f, circle.transform.localScale.z - 15f * (Time.deltaTime * 4));
        }
    }

    /// <summary>
    /// Extend the size of the circle
    /// </summary>
    /// <param name="circle">GameObject of the current circle</param>
    private void ExtendCircle(GameObject circle)
    {
        //if it's smaller than the max circle size
        if (circle.transform.localScale.x < 30)
        {
            //2f * Time.deltaTime so the computers speed doesn't affect the speed
            circle.transform.localScale =
            new Vector3(circle.transform.localScale.x + 15f * (Time.deltaTime * 4),
            0.1f, circle.transform.localScale.z + 15f * (Time.deltaTime * 4));
        }
    }
}