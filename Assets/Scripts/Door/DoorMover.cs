using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DoorMover : MonoBehaviour
{
    [SerializeField] private Vector3 _openingOffset = new Vector3(0f, -3f, 0f);
    [SerializeField] private float _openingTime = 1.5f;
    private DoorMoverState _state = DoorMoverState.Closed;

    public void StartOpeningDoor()
    {
        if(_state != DoorMoverState.Closed)
            return;
        _state = DoorMoverState.Moving;
        StartCoroutine(MoveDoor(transform.position, transform.position + _openingOffset, DoorMoverState.Open));
    }
    
    public void StartClosingDoor()
    {
        if(_state != DoorMoverState.Open)
            return;
        _state = DoorMoverState.Moving;
        StartCoroutine(MoveDoor(transform.position, transform.position - _openingOffset, DoorMoverState.Closed));
    }

    private IEnumerator MoveDoor(Vector3 startPosition, Vector3 finishPosition, DoorMoverState stateAfterMoving)
    {
        float startTime = Time.time;
        while(Time.time < startTime + _openingTime)
        {
            transform.position = Vector3.Lerp(startPosition, finishPosition, (Time.time - startTime) / _openingTime);
            yield return null;
        }
        _state = stateAfterMoving;
    }

    private enum DoorMoverState
    {
        Closed,
        Open,
        Moving
    }
}


