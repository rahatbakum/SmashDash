using UnityEngine;

public class TargetOffsetFollower : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float speedCoef = 6f;
    private Vector3 _offset;

    private void Start()
    {
        _offset = _target.position - transform.position;
    }

    private void FixedUpdate()
    {
        if(_target != null)
            transform.position = Vector3.Lerp(transform.position, _target.position - _offset, speedCoef * Time.deltaTime);
    }
}
