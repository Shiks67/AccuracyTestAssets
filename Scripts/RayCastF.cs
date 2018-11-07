using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastF : MonoBehaviour
{

    private Transform myCamera;
    public Ray ray;
    // Use this for initialization
    void Start()
    {
        myCamera = GameObject.FindGameObjectWithTag("EditorOnly").transform;
    }

    // Update is called once per frame
    void Update()
    {
        ray = new Ray(myCamera.position,
        myCamera.rotation * Vector3.forward * 15);
        Debug.DrawRay(myCamera.position,
        myCamera.rotation * Vector3.forward * 15, Color.red);
    }
}
