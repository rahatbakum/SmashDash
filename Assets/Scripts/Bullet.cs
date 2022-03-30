using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bullet : MonoBehaviour, IExplodingObject, IMassObject
{
    private const float ExplodeSizeCoefficient = 3f;
    private const float MinDistance = 0.05f;

    
    [SerializeField] private float _speed = 7f;
    [SerializeField] private UnityEvent _exploding;
    public event UnityAction Exploding
    {
        add => _exploding.AddListener(value);
        remove => _exploding.RemoveListener(value);
    }

    [SerializeField] private UnityEvent _exploded;
    public event UnityAction Exploded
    {
        add => _exploded.AddListener(value);
        remove => _exploded.RemoveListener(value);
    }

    [SerializeField] private UnityEvent<float> _massChanged;
    public event UnityAction<float> MassChanged
    {
        add => _massChanged.AddListener(value);
        remove => _massChanged.RemoveListener(value);
    }

    private float _mass;
    public float Mass
    {
        get => _mass;
        set
        {
            _mass = value;
            _massChanged.Invoke(_mass);
        }
    }
    public float CurrentRadius
    {
        get => MassApplier.MassToRadius(_mass);
    }

    private SphereCollider _sphereCollider;
    private Vector3 _targetPosition;
    private Vector3 _previousPosition;
    private float _startPlayerRadius;


    //Call this from another object after instantiation
    public void Initialize(Vector3 targetPosition, float startMass = 0f)
    {
        _targetPosition = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);
        Mass = startMass;
    }

    private void Awake()
    {
        _previousPosition = transform.position;
        _sphereCollider = GetComponentInChildren<SphereCollider>();
    }

    private void MoveAfterOneFrame()
    {
        _previousPosition = transform.position;
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime);
    }

    //use this to check line from previous position to current
    private bool IsTouchedObstacle()
    {
        Collider[] colliders = Physics.OverlapCapsule(_previousPosition, transform.position, CurrentRadius);
        foreach(var item in colliders)
        {
            if(Obstacle.TryGetObstacle(item, out Obstacle obstacle))
                return true;
        }
        return false;
    }

    private bool IsInTargetPositon()
    {
        return Vector3.Distance(transform.position, _targetPosition) <= MinDistance;
    }

    private IEnumerator Fly()
    {
        while(true)
        {
            MoveAfterOneFrame();
            if(IsTouchedObstacle() || IsInTargetPositon())
            {
                Explode();
                yield break;
            }
            yield return null;
        }
    }

    public void Shoot()
    {
        StartCoroutine(Fly());
    }

    private float GetExlodingRadius()
    {
        return ExplodeSizeCoefficient * CurrentRadius;
    }

    public void Explode()
    {
        _exploding.Invoke();
        Collider[] colliders = Physics.OverlapSphere(transform.position, GetExlodingRadius());
        foreach(var item in colliders)
        {
            if(Obstacle.TryGetObstacle(item, out Obstacle obstacle))
            {
                obstacle.Explode();
            }
        }
        _exploded.Invoke();
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_sphereCollider.transform.position, GetExlodingRadius());
    }
}
