using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeMarker : MonoBehaviour
{

    public GameObject gazeDotMap;
    public GameObject dotMark;
    public static List<GameObject> listDotMark = new List<GameObject>();
    public static List<GameObject> oldListDotMark = new List<GameObject>();
    private RayCaster rCaster;
    private Vector3 pos;
    private float timer = 0.1f;

    void Start()
    {
        // rCaster = GameObject.FindGameObjectWithTag("EditorOnly").GetComponent<RayCastF>();
        rCaster = Camera.main.GetComponent<RayCaster>();
    }

    // Update is called once per frame
    void Update()
    {
        // GameObject hitObject;
        // RaycastHit[] hits;

        // //everything that the raycast hit
        // hits = Physics.RaycastAll(rCaster.ray);
        // for (int i = 0; i < RayCaster.hits.Length; i++)
        // {
        //     RaycastHit hit = RayCaster.hits[i];
        //     // hitObject = hit.collider.gameObject;
        //     timer -= Time.deltaTime;
        //     if (hit.transform.name == "Quadri")
        //     {
        //         gameObject.transform.localPosition = transform.InverseTransformPoint(hit.point); ;
        //         print(gameObject.transform.localPosition);
        //     }
        //     //only move the gazeMarker on the background's collider
        //     //avoid the cylinder's collider which is a capsule and get smaller 
        //     //which make the gazemarker move
        //     // if (hitObject.name == "Quadri")
        //     // {
        //     //     gameObject.transform.position = hit.transform.InverseTransformPoint(hit.point);
        //     //     print(gameObject.transform.localPosition);
        //     //     //save the position for gaze dots
        //     //     pos = hit.point;
        //     // }
        //     //if a circle is hitted and the timer is at 0
        //     //create a dot at the position on the background's collider
        //     // // // if (SpawnCircle.targetCircle.Contains(hitObject) &&
        //     // // // SpawnCircle.targetCircle.Count == 1 && timer < 0)
        //     // // // {
        //     // // //     GameObject newObject = Instantiate(dotMark);
        //     // // //     //set parent so the dots stay at the same place on the grid
        //     // // //     newObject.transform.SetParent(gazeDotMap.transform);
        //     // // //     //position from the hit.point on the background
        //     // // //     newObject.transform.position = pos;
        //     // // //     newObject.GetComponent<Renderer>().material.color = Color.black;
        //     // // //     listDotMark.Add(newObject);
        //     // // //     timer = 0.1f;
        //     // // // }
        // }
    }
}
