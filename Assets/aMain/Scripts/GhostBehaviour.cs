using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBehaviour : MonoBehaviour
{

    public float lerpSpeed = 2;
    public int frameUpdate = 20;

    private int _index = 1;
    private bool _noOverflow = true;

    private Vector3 _actualPos;
    private Vector3 _nextPos;
    
    private float _startTime;
    private float _journeyLength;
    private float _distCovered;
    private float _fracJourney;
    private ObjectPooler _objPooler;

    private void Start()
    {
        _objPooler = ObjectPooler.Instance;
        frameUpdate = GhostData.frameUpdate;
        transform.GetComponent<MeshRenderer>().material.SetColor("_Color",GhostData.initColor);
    }

    private void FixedUpdate()
    {
        if (FixedTime.fixedFrameCount % frameUpdate == 0 && _noOverflow)
        {
            _startTime = Time.time;

            _actualPos = transform.localPosition;
            _nextPos = GhostData.posTracker[_index];
            if (_nextPos == null)
            {
                _nextPos = _actualPos;
            }
            
            _journeyLength = Vector3.Distance(_actualPos, _nextPos);

            if (_index >= GhostData.posTracker.Count - 1)
            {
                _noOverflow = false;
            }
            _index++;
        }

        if (FixedTime.fixedFrameCount % (frameUpdate / 2) == 0 && _noOverflow)
        {
            _objPooler.SpawnFromPool("Cuce", transform.position, Quaternion.identity);
        }


        if (_noOverflow)
        {
            _distCovered = (Time.time - _startTime) * lerpSpeed;
            _fracJourney = _distCovered / _journeyLength;
            transform.localPosition = Vector3.Lerp(_actualPos, _nextPos, _fracJourney);

        }

        
    }
}
