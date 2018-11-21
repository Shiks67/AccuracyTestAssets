using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusHelper : MonoBehaviour
{

    public GameObject targetHelper;
    private GameObject newObject;
    // Use this for initialization
    void Start()
    {
        if(SpawnCircle.targetCircle.Count != 1)
            return;
        newObject = Instantiate(targetHelper);
        newObject.transform.SetParent(gameObject.transform);
        newObject.transform.localScale = new Vector3(1, 1, 1);
        newObject.transform.localRotation = Quaternion.Euler(90, 0, 0);
        newObject.transform.localPosition = new Vector3(0, -2, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(newObject == null)
            return;

        newObject.transform.localScale = new Vector3(newObject.transform.localScale.x - 0.15f * (Time.deltaTime * 4),
            newObject.transform.localScale.y - 0.15f * (Time.deltaTime * 4), 1);

        if (newObject.transform.localScale.x < 0.05 || gameObject.transform.localScale.x != 30)
            Destroy(newObject);
    }
}
