using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UserID : MonoBehaviour
{

    public Text userIDText;
    
    public void SetUserID()
    {
        //save the user id from the pupil lab calibration scene to use it in another scene/scripts
        PlayerPrefs.SetString("UserID", userIDText.text);
    }
}
