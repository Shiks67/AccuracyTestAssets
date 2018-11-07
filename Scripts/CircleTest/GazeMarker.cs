using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeMarker : MonoBehaviour
{

    private RayCastF rCaster;
    public GameObject backgroundCollider;
    public GameObject dotMark;
    private Vector3 pos;

    void Start()
    {
        rCaster = GameObject.FindGameObjectWithTag("EditorOnly").GetComponent<RayCastF>();
        // rCaster = Camera.main.GetComponent<RayCaster>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject hitObject;
        RaycastHit[] hits;
        hits = Physics.RaycastAll(rCaster.ray);
        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            hitObject = hit.collider.gameObject;

            if (hitObject == backgroundCollider)
            {
                gameObject.transform.position = hit.point;
                pos = hit.point;
            }
            if (hitObject == GameObject.FindGameObjectWithTag("hitCircle"))
            {
                GameObject newObject = Instantiate(dotMark);
                newObject.transform.SetParent(GameObject.Find("GazeDotMap").transform);
                newObject.transform.position = pos;
                newObject.GetComponent<Renderer>().material.color = Color.black;
            }
        }
    }
}
