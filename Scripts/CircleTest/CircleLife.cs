using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CircleLife : MonoBehaviour
{
    public float TTFF;
    public bool isTTFF;
    private SpawnCircle sc;
    private int index;
    public void Init(int index)
    {
        this.index = index;
    }

    // Use this for initialization
    void Start()
    {
        var quadri = GameObject.Find("Quadri");
        sc = quadri.GetComponent<SpawnCircle>();
        isTTFF = true;
    }

    // Update is called once per frame
    void Update()
    {
        ColorLevel();
        //if there is more than 1 circle, return
        if (GameObject.FindGameObjectsWithTag("hitCircle").Length > 1)
            return;
        if (isTTFF) //update TTFF time until the circle is focused by the gaze point
            TTFF += Time.deltaTime;
        if (Input.GetKeyUp(KeyCode.Space))
        {
            //save the circle's scale and destroy it
            sc.circleFinalSize[index] = gameObject.transform.localScale.x;
            Destroy(gameObject);
        }
    }

    //Change the color of the circle compared to his size
    private void ColorLevel()
    {
        if (gameObject.transform.localScale.x > 20)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.red;
        }
        else if (gameObject.transform.localScale.x > 10)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.blue;
        }
        else if (gameObject.transform.localScale.x > 0)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.green;
        }
    }
}
