using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{

    private RayCaster rCaster;
    private RaycastHit hit;
    // private GameObject mainCamera;
    private SpawnCircle sc;
    private bool dotMarkVisibility;

    // Use this for initialization
    void Start()
    {
        // rCaster = GameObject.FindGameObjectWithTag("EditorOnly").GetComponent<RayCastF>();
        rCaster = Camera.main.GetComponent<RayCaster>();
        var spawnCircle = GameObject.Find("Quadri");
        sc = spawnCircle.GetComponent<SpawnCircle>();
        // SceneManager.UnloadSceneAsync("CircleTest 1");
    }

    // Update is called once per frame
    void Update()
    {
        //show/hide gaze dots
        if (Input.GetKeyUp(KeyCode.O))
        {
            dotMarkVisibility = !dotMarkVisibility;
            foreach (var dot in GazeMarker.listDotMark)
            {
                dot.GetComponent<Renderer>().enabled = dotMarkVisibility;
            }
            foreach (var dot in GazeMarker.oldListDotMark)
            {
                dot.GetComponent<Renderer>().enabled = dotMarkVisibility;
            }
        }
        if (Physics.Raycast(rCaster.ray, out hit))
        {
            //if there is max 1 circle on the grid
            if (SpawnCircle.targetCircle.Count == 1)
            {
                //Switch on hitted object's tag by the gaze
                switch (hit.transform.gameObject.tag)
                {
                    case "hitCircle": //if the tag is hitCircle, reduce the size of the circle
                        ReduceCircle(hit.transform.gameObject);
                        break;
                    case "Finish": //show the result, size of every circle
                        sc.Result();
                        break;
                    case "Respawn": //reset all circles so we can try again
                        sc.DestroyAllCircles(true);
                        break;
                    default: //by default we extend the size of the circle
                        ExtendCircle(SpawnCircle.targetCircle.First());
                        return;
                }
            }
            else
            {
                //after Retry button is focused
                if (hit.transform.gameObject.tag == "Respawn")
                {
                    sc.DestroyAllCircles(true);
                }
                //after Resume button is focused
                if (hit.transform.gameObject.tag == "Resume")
                {
                    sc.DestroyAllCircles();
                }
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
            new Vector3(circle.transform.localScale.x + 2f * Time.deltaTime,
            0.1f, circle.transform.localScale.z + 2f * Time.deltaTime);
        }
    }
}