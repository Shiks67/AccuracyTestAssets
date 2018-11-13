using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class FovCalibration : MonoBehaviour
{

    public GameObject UpLeft;
    public GameObject UpRight;
    public GameObject DownLeft;
    public GameObject DownRight;
    private RayCaster rCaster;
    private int speed = 5;
    public List<Text> verticalTextList = new List<Text>();
    public List<Text> horizontalTextList = new List<Text>();

    // Use this for initialization
    void Start()
    {
        // rCaster = GameObject.FindGameObjectWithTag("EditorOnly").GetComponent<RayCastF>();
        rCaster = Camera.main.GetComponent<RayCaster>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.O))
            RandomizeText();
        RaycastHit hit;
        if (Physics.Raycast(rCaster.ray, out hit))
        {
            var obj = hit.transform.gameObject;

            if (obj == UpLeft)
            {
                hit.transform.localPosition = new Vector3(hit.transform.localPosition.x - speed * Time.deltaTime,
                    hit.transform.localPosition.y + speed * Time.deltaTime, hit.transform.localPosition.z);
            }
            else if (obj == DownLeft)
            {
                hit.transform.localPosition = new Vector3(hit.transform.localPosition.x - speed * Time.deltaTime,
                    hit.transform.localPosition.y - speed * Time.deltaTime, hit.transform.localPosition.z);
            }
            else if (obj == DownRight)
            {
                hit.transform.localPosition = new Vector3(hit.transform.localPosition.x + speed * Time.deltaTime,
                    hit.transform.localPosition.y - speed * Time.deltaTime, hit.transform.localPosition.z);
            }
            else if (obj == UpRight)
            {
                hit.transform.localPosition = new Vector3(hit.transform.localPosition.x + speed * Time.deltaTime,
                    hit.transform.localPosition.y + speed * Time.deltaTime, hit.transform.localPosition.z);
            }
            else if (obj.name == "Up")
            {
                hit.transform.parent.localPosition = new Vector3(hit.transform.parent.localPosition.x,
                    hit.transform.parent.localPosition.y + speed * Time.deltaTime, hit.transform.parent.localPosition.z);
            }
            else if (obj.name == "Left")
            {
                hit.transform.parent.localPosition = new Vector3(hit.transform.parent.localPosition.x - speed * Time.deltaTime,
                    hit.transform.parent.localPosition.y, hit.transform.parent.localPosition.z);
            }
            else if (obj.name == "Right")
            {
                hit.transform.parent.localPosition = new Vector3(hit.transform.parent.localPosition.x + speed * Time.deltaTime,
                    hit.transform.parent.localPosition.y, hit.transform.parent.localPosition.z);
            }
            else if (obj.name == "Down")
            {
                hit.transform.parent.localPosition = new Vector3(hit.transform.parent.localPosition.x,
                    hit.transform.parent.localPosition.y - speed * Time.deltaTime, hit.transform.parent.localPosition.z);
            }
        }
    }
    private string randomText = "";

    private void RandomizeText()
    {
        foreach (var htext in horizontalTextList)
        {
            for (int i = 0; i < 11; i++)
            {
                randomText += (char)Random.Range(65, 91) + " ";
            }
            htext.text = randomText;
            randomText = "";
        }
        foreach (var vtext in verticalTextList)
        {
            for (int i = 0; i < 8; i++)
            {
                randomText += (char)Random.Range(65, 91) + "\n";
            }
            vtext.text = randomText;
            randomText = "";
        }
    }
}
