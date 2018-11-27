// originally created by Theo and Kiefer (French interns at AAU Fall 2017) 
// modified by Bianca

// outcommented parts doesn't work in Pupil Labs Plugin, but is used in another project

using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;

namespace RockVR.Video.Demo
{
    public class LoggerBehavior : MonoBehaviour
    {

        private static Logger _logger;
        private static List<object> _toLog;
        private Vector3 gazeToWorld;
        private static string CSVheader = AppConstants.CsvFirstRow;
        private Camera dedicatedCapture;

        //CircleTruc log var
        private GameObject circleObject;
        private string gazePosx, gazePosy;
        private float timer;
        private float TTFF;
        public GameObject GazePosObj;

        //private Camera dedicatedCapture;

        public static string sceneName = "_";

        #region Unity Methods

        private void Start()
        {
            _toLog = new List<object>();
            dedicatedCapture = Camera.main;
        }

        private void Update()
        {
            if (SpawnCircle.targetCircle.Count == 1)
            {
                gazePosx = (GazePosObj.transform.localPosition.x * 10).ToString("F2");
                gazePosy = (GazePosObj.transform.localPosition.y * 10).ToString("F2");
                CircleInfo();
                DoLog();
                AddToLog();
            }
        }

        private void CircleInfo()
        {
            if (circleObject != SpawnCircle.targetCircle.First())
                circleObject = SpawnCircle.targetCircle.First();
            if (!circleObject.GetComponent<CircleLife>().isTTFF)
                TTFF = circleObject.GetComponent<CircleLife>().TTFF;
            else
                TTFF = 0;
            timer += Time.deltaTime;
        }

        private void AddToLog()
        {
            if (PupilData._2D.GazePosition != Vector2.zero)
            {
                gazeToWorld = dedicatedCapture.ViewportToWorldPoint(new Vector3
                (PupilData._2D.GazePosition.x, PupilData._2D.GazePosition.y,
                Camera.main.nearClipPlane));
            }

            //var raycastHit = EyeRay.CurrentlyHit;
            var tmp = new
            {
                aUserID = _logger.userID,
                aDate = _logger.FolderName.Replace('-', '/') + " - " + _logger.FileName.Replace('-', ':'),
                // default variables for all scenes
                a = Math.Round(timer, 3),
                fps = (int)(1.0f / Time.unscaledDeltaTime), //frames per second during the last frame, could calculate an average frame rate instead

                circleXpos = circleObject != null ? Math.Round(circleObject.transform.localPosition.x, 3) : double.NaN,
                circleYpos = circleObject != null ? Math.Round(circleObject.transform.localPosition.y, 3) : double.NaN,

                j = PupilData._2D.GazePosition != Vector2.zero ? Math.Round(PupilData._2D.GazePosition.x, 3) : double.NaN,
                k = PupilData._2D.GazePosition != Vector2.zero ? Math.Round(PupilData._2D.GazePosition.y, 3) : double.NaN,
                // l = PupilData._2D.GazePosition != Vector2.zero ? Math.Round(gazeToWorld.x, 3) : double.NaN,
                // m = PupilData._2D.GazePosition != Vector2.zero ? Math.Round(gazeToWorld.y, 3) : double.NaN,
                lbis = gazePosx,
                mbis = gazePosy,
                // n = PupilData._2D.GazePosition != Vector2.zero ? Math.Round(PupilTools.FloatFromDictionary(PupilTools.gazeDictionary, "confidence"), 3) : double.NaN, // confidence value calculated after calibration 
                confLeft = PupilData._2D.GazePosition != Vector2.zero ? Math.Round(PupilInfo.confidence1, 3) : double.NaN, // confidence value calculated after calibration 
                confRight = PupilData._2D.GazePosition != Vector2.zero ? Math.Round(PupilInfo.confidence0, 3) : double.NaN, // confidence value calculated after calibration 
                confGaze = PupilData._2D.GazePosition != Vector2.zero ? Math.Round(PupilInfo.gazeConfidence, 3) : double.NaN,

                circleSize = circleObject != null ? Math.Round(circleObject.transform.localScale.x, 3) : double.NaN,
                TimeToFirstFix = TTFF != 0 ? Math.Round(TTFF, 3) : double.NaN
            };
            _toLog.Add(tmp);
        }

        private Vector3 CalculEyeGazeOnObject(RaycastHit hit)
        {
            return hit.transform.InverseTransformPoint(hit.point);
        }

        public static void DoLog()
        {
            CSVheader = AppConstants.CsvFirstRow;
            _logger = Logger.Instance;
            if (_toLog.Count == 0)
            {
                var firstRow = new { CSVheader };
                _toLog.Add(firstRow);
            }
            _logger.Log(_toLog.ToArray());
            _toLog.Clear();
        }

        #endregion
    }
}