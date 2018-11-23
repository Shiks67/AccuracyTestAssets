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
    // private bool dotMarkVisibility;
    public GameObject menu;

    // Use this for initialization
    void Start()
    {
        // rCaster = GameObject.FindGameObjectWithTag("EditorOnly").GetComponent<RayCastF>();
        rCaster = Camera.main.GetComponent<RayCaster>();
        // SceneManager.UnloadSceneAsync("CircleTest 1");
    }

    // Update is called once per frame
    void Update()
    {
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
            else
            {
                if (Input.GetKeyUp(KeyCode.F))
                    StartCoroutine(LoadCurrentScene());
                    SceneManager.UnloadSceneAsync("CircleTest");
            }
        }
    }

    IEnumerator LoadCurrentScene()
    {
        AsyncOperation asyncScene = SceneManager.LoadSceneAsync("Field of view"
            , LoadSceneMode.Additive);

        while (!asyncScene.isDone)
        {
            yield return null;
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