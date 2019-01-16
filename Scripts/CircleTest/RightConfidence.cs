using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class RightConfidence : MonoBehaviour
{
	//Update current confidence of the right eye text
    void Update()
    {
        this.gameObject.GetComponent<Text>().text = "Right confidence\n" + (PupilLabData.confidence0 * 100) + "%";
    }
}
