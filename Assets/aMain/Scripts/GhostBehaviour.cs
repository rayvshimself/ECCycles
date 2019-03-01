using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBehaviour : MonoBehaviour
{
    public float lerpSpeed = 2;
    public int frameUpdate = 20;

    private int index = 1;
    private bool noOverflow = true;

    private Vector3 actualPos;
    private Vector3 nextPos;
    
    private float startTime;
    private float journeyLength;
    private float distCovered;
    private float fracJourney;

    private void Start()
    {
        frameUpdate = GhostData.frameUpdate;
        transform.GetComponent<MeshRenderer>().material.SetColor("_Color",GhostData.initColor);
    }

    private void FixedUpdate()
    {
        if (FixedTime.fixedFrameCount % frameUpdate == 0 && noOverflow)
        {
            startTime = Time.time;

            actualPos = transform.localPosition;
            nextPos = GhostData.posTracker[index];
            if (nextPos == null)
            {
                nextPos = actualPos;
            }
            
            journeyLength = Vector3.Distance(actualPos, nextPos);

            if (index >= GhostData.posTracker.Count - 1)
            {
                noOverflow = false;
            }
            index++;
        }


        if (noOverflow)
        {
            distCovered = (Time.time - startTime) * lerpSpeed;
            fracJourney = distCovered / journeyLength;
            transform.localPosition = Vector3.Lerp(actualPos, nextPos, fracJourney);

        }

        
    }
}
