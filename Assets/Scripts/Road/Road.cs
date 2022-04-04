using UnityEngine;

public class Road : MonoBehaviour
{

    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _mainRoad;
    [SerializeField] private Transform _finishPoint;

    public void UpdateWidth(float width)
    {
        UpdateMainRoadWidth(width);
        UpdateStartPointWidth(width);
        UpdateFinishPointWidth(width);
    }

    public void UpdateLength(Vector3 startPosition, Vector3 finishPosition)
    {
        Vector3 newStartPosition = new Vector3(startPosition.x, _startPoint.position.y, startPosition.z);
        Vector3 newFinishPosition = new Vector3(finishPosition.x, _finishPoint.position.y, finishPosition.z);
        UpdateMainRoadPoints(newStartPosition, newFinishPosition);
        UpdateStartPointPosition(newStartPosition);
        UpdateFinishPointPosition(newFinishPosition);
    }

    private void UpdateMainRoadPoints(Vector3 newStartPosition, Vector3 newFinishPosition)
    {
        _mainRoad.position = MathHelper.MiddlePoint(newStartPosition, newFinishPosition);
        _mainRoad.rotation = Quaternion.FromToRotation(Vector3.forward, newStartPosition - newFinishPosition);
        _mainRoad.localScale = new Vector3(_mainRoad.localScale.x, _mainRoad.localScale.y, Vector3.Distance(newStartPosition, newFinishPosition));
    }

    private void UpdateStartPointPosition(Vector3 newPosition)
    {
        _startPoint.position = newPosition;
    }
    
    private void UpdateFinishPointPosition(Vector3 newPosition)
    {
        _finishPoint.position = newPosition;
    }

    private void UpdateMainRoadWidth(float newWidth)
    {
        _mainRoad.localScale = new Vector3(newWidth, _mainRoad.localScale.y, _mainRoad.localScale.z);
    }

    private void UpdateStartPointWidth(float newWidth)
    {
        _startPoint.localScale = new Vector3(newWidth, _startPoint.localScale.y, newWidth);
    }
    
    private void UpdateFinishPointWidth(float newWidth)
    {
        _finishPoint.localScale = new Vector3(newWidth, _finishPoint.localScale.y, newWidth);
    }
}
