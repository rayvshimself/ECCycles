﻿using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class GhostData : MonoBehaviour
{
    public static List<Vector3> posTracker = new List<Vector3>();
    //List<GameObject> ghostList = new List<GameObject>();
    public static int frameUpdate = 20;
    
    public GameObject trackingStart;
    public GameObject trackingEnd;
    public GameObject ghostPrefab;

    private Collider _startCol;
    private Collider _endCol;
    private bool trackingBool = false;

    private bool replayBool = false;
//    private int index;
//    private bool noOverflow = true;
//
//    private GameObject ghost;
//    private int ghostCount = 0;
//    private Vector3 actualPos;
//    private Vector3 nextPos;
//    private float startTime;
//    private float _timeStartetdLerping;
//    private float journeyLength;
//
//    private float distCovered;
//    private float fracJourney;

    private void Awake()
    {
        trackingBool = true;
        //_startCol = trackingStart.GetComponent<Collider>();
        _endCol = trackingEnd.GetComponent<Collider>();
    }


    private void OnTriggerEnter(Collider other)
    {

//        if (other == _startCol)
//        {
//            trackingBool = true;
//            //start writing to List
//        }

        if (other == _endCol)
        {
            // ghostList.Add(Instantiate(ghostPrefab, posTracker[1], Quaternion.identity));
            Instantiate(ghostPrefab, posTracker[1], Quaternion.identity);

            //ghost = Instantiate(ghostPrefab, posTracker[1], Quaternion.identity);
            //replayBool = true;
            //stop writing in List and playback
        }
    }

    private void FixedUpdate()
    {
        if ((posTracker.Count - 1) >= 1000)
        {
            trackingBool = false;
        }
        
        if (FixedTime.fixedFrameCount % frameUpdate == 0 && trackingBool)
        {
            posTracker.Add(this.transform.localPosition);
            //Debug.Log("posTracker Count: " + posTracker.Count);
        }
    }

//    private void FixedUpdate()
//    {
//        if ((posTracker.Count - 1) >= 1000)
//        {
//            trackingBool = false;
//        }
//
//        if (FixedTime.fixedFrameCount % frameUpdate == 0 && trackingBool)
//        {
//            posTracker.Add(this.transform.localPosition);
//            Debug.Log("posTracker Count: " + posTracker.Count);
//        }
//
//        if (ghost != null)
//        {
//            if (FixedTime.fixedFrameCount % frameUpdate == 0 && replayBool && noOverflow)
//            {
//                startTime = Time.time;
//                //actualPos = posTracker[index];
//
//                actualPos = ghost.transform.localPosition;
//                nextPos = posTracker[index];
//                journeyLength = Vector3.Distance(actualPos, nextPos);
//                //index[i]++;
//            
//                if (index >= posTracker.Count-1)
//                {
//                    noOverflow = false;
//                }
//                index++;
//            }
//
//        
//            if (replayBool && noOverflow)
//            {
////            float timeSinceStarted = Time.time - _timeStartetdLerping;
////            float percentageComplete = timeSinceStarted / 1f;
////            Debug.Log(percentageComplete);
////            ghost.transform.localPosition = Vector3.Lerp(actualPos,nextPos, percentageComplete);
//            
//
//                //Debug.Log(fracJourney);
//
//                distCovered = (Time.time - startTime) * lerpSpeed;
//                fracJourney = distCovered / journeyLength;
//                ghost.transform.localPosition = Vector3.Lerp(actualPos,nextPos, fracJourney);
//
//
//            }
//            
//        }
//    }



}
