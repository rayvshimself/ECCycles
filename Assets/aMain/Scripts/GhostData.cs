using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GhostData : MonoBehaviour
{
    List<Vector3> posTracker = new List<Vector3>();

    public GameObject trackingStart;
    public GameObject trackingEnd;
    public GameObject ghostPrefab;

    private Collider _startCol;
    private Collider _endCol;
    private bool trackingBool = false;
    private bool replayBool = false;
    private int index = 0;
    private bool noOverflow = true;

    private GameObject ghost;

    private void Awake()
    {
        _startCol = trackingStart.GetComponent<Collider>();
        _endCol = trackingEnd.GetComponent<Collider>();

        
    }




    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        
        if (other == _startCol)
        {
            trackingBool = true;
            //start writing to List
        }

        if (other == _endCol)
        {
            trackingBool = false;
            replayBool = true;

            ghost = Instantiate(ghostPrefab, posTracker[0], Quaternion.identity);
            //stop writing in List and playback
        }
    }

    private void FixedUpdate()
    {

        //Debug.Log("tracking: " + trackingBool + " _ replay: " + replayBool);
        
        if (FixedTime.fixedFrameCount % 3 == 0 && trackingBool)
        {
            posTracker.Add(this.transform.localPosition);
            //Debug.Log("posTracker Count: " + posTracker.Count);
        }

        if (FixedTime.fixedFrameCount % 6 == 0 && replayBool && noOverflow)
        {
            Debug.Log(index);
            ghost.transform.localPosition = posTracker[index];
            index++;
            

            if (index >= posTracker.Count-1)
            {
                noOverflow = false;
            }
        }
    }
}
