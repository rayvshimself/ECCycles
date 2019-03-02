using System.Collections.Generic;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class GhostData : MonoBehaviour
{
    public static List<Vector3> posTracker = new List<Vector3>();
    public static int frameUpdate = 20;
    public static Color initColor;

    public GameObject trackingEnd;
    public GameObject ghostPrefab;

    private Collider _endCol;
    private bool trackingBool = false;
    private int ghostCount = 0;

    private void Awake()
    {
        _endCol = trackingEnd.GetComponent<Collider>();
        
        //Start tracking
        trackingBool = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        //if the endcollider is triggered spawn a ghost, unless the max ghost count is reached
        if (other == _endCol && ghostCount <= 10)
        {

            Instantiate(ghostPrefab, posTracker[1], Quaternion.identity);
            ghostCount++;
            
            initColor = new Color(1f/ghostCount,0.5f,0.5f);
        }
    }

    private void FixedUpdate()
    {
        //stop filling in tracking points into the List posTracker
        if ((posTracker.Count - 1) >= 3600)
        {
            trackingBool = false;
        }
        
        //Fills in positions into List every specified Update
        if (FixedTime.fixedFrameCount % frameUpdate == 0 && trackingBool)
        {
            posTracker.Add(this.transform.localPosition);
            //Debug.Log("posTracker Count: " + posTracker.Count);
        }
    }

}
