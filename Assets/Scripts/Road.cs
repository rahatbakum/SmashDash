using UnityEngine;

public class Road : MonoBehaviour
{

    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _mainRoad;
    [SerializeField] private Transform _finishPoint;

    public void UpdateWidth(float width)
    {
        _startPoint.localScale = new Vector3(width, _startPoint.localScale.y, width);
        _mainRoad.localScale = new Vector3(width, _mainRoad.localScale.y, _mainRoad.localScale.z);
        _finishPoint.localScale = new Vector3(width, _finishPoint.localScale.y, width);
    }

    public void UpdateLength(Vector3 startPosition, Vector3 finishPosition)
    {
        Vector3 newStartPosition = new Vector3(startPosition.x, _startPoint.position.y, startPosition.z);
        Vector3 newFinishPosition = new Vector3(finishPosition.x, _finishPoint.position.y, finishPosition.z);
        _mainRoad.position = (newStartPosition + newFinishPosition) / 2f;
        _mainRoad.rotation = Quaternion.FromToRotation(Vector3.forward, newStartPosition - newFinishPosition);
        _mainRoad.localScale = new Vector3(_mainRoad.localScale.x, _mainRoad.localScale.y, Vector3.Distance(newStartPosition, newFinishPosition));
        _startPoint.position = newStartPosition;
        _finishPoint.position = newFinishPosition;
    }
}
