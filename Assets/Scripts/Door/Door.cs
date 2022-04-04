using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private DoorMover _doorMover;
    [SerializeField] private float _minDistance = 5f;

    public void OnPlayerMove(Vector3 position)
    {
        if(Vector3.Distance(_doorMover.transform.position, position) <= _minDistance)
            _doorMover.StartOpeningDoor();
    }

    private void Win()
    {
        _doorMover.StartClosingDoor();
        GameManager.Instance?.Win();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(Player.TryGetPlayer(other, out Player player))
            Win();
    }
}
