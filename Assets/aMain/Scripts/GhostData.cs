using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class GhostData : MonoBehaviour
{
    List<Vector3> posTracker = new List<Vector3>();

    public GameObject trackingStart;
    public GameObject trackingEnd;
    public GameObject ghostPrefab;
    public float lerpSpeed = 1;
    public int frameUpdate = 15;

    private Collider _startCol;
    private Collider _endCol;
    private bool trackingBool = false;
    private bool replayBool = false;
    private int index = 0;
    private bool noOverflow = true;

    private GameObject ghost;
    private Vector3 actualPos;
    private Vector3 nextPos;
    private float startTime;
    private float _timeStartetdLerping;
    private float journeyLength;


    private void Awake()
    {
        _startCol = trackingStart.GetComponent<Collider>();
        _endCol = trackingEnd.GetComponent<Collider>();
    }


    private void OnTriggerEnter(Collider other)
    {
        
        if (other == _startCol)
        {
            trackingBool = true;
            //start writing to List
        }

        if (other == _endCol)
        {
            ghost = Instantiate(ghostPrefab, posTracker[0], Quaternion.identity);
            //trackingBool = false;
            replayBool = true;
            //stop writing in List and playback
        }
    }
    
    private void Update()
    {
        if (replayBool && noOverflow && ghost != null)
        {
            _timeStartetdLerping = Time.time;
        }
    }

    private void FixedUpdate()
    {

        if (FixedTime.fixedFrameCount % frameUpdate == 0 && trackingBool)
        {
            posTracker.Add(this.transform.localPosition);
            Debug.Log("posTracker Count: " + posTracker.Count);
        }

        if (FixedTime.fixedFrameCount % frameUpdate == 0 && replayBool && noOverflow)
        {
            startTime = Time.time;
//          actualPos = posTracker[index];
            actualPos = ghost.transform.localPosition;
            nextPos = posTracker[index];
            journeyLength = Vector3.Distance(actualPos, nextPos);


            if (index >= posTracker.Count-1)
            {
                noOverflow = false;
            }
            index++;
        }
        
        if (replayBool && noOverflow && ghost != null)
        {
//            float timeSinceStarted = Time.time - _timeStartetdLerping;
//            float percentageComplete = timeSinceStarted / 1f;
//            Debug.Log(percentageComplete);
//            ghost.transform.localPosition = Vector3.Lerp(actualPos,nextPos, percentageComplete);
            
            float distCovered = (Time.time - startTime) * lerpSpeed;
            float fracJourney = distCovered / journeyLength;
            //Debug.Log(fracJourney);
            ghost.transform.localPosition = Vector3.Lerp(actualPos,nextPos, fracJourney);
        }
        
    }
}
