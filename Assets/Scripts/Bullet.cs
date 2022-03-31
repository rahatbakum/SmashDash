using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class Bullet : MonoBehaviour, IExplodingObject, IMassObject
{
    private const float ExplodeSizeCoef = 5f;
    private const float ExplodeSizePow = 1.5f;
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

    private void MoveAfterOneFrame(Vector3 targetPosition)
    {
        _previousPosition = transform.position;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, _speed * Time.deltaTime);
    }

    private bool IsInTargetPositon(Vector3 targetPosition)
    {
        return Vector3.Distance(transform.position, targetPosition) <= MinDistance;
    }

    private IEnumerator Fly(Vector3 targetPosition)
    {
        while(true)
        {
            MoveAfterOneFrame(targetPosition);
            if(IsInTargetPositon(targetPosition))
            {
                Explode();
                yield break;
            }
            yield return null;
        }
    }

    public void Shoot()
    {
        Predicate<Collider> condition = (Collider collider) => Obstacle.TryGetObstacle(collider, out Obstacle obstacle);
        Collider nearestObstacle = WayCollisionDetector.GetFirstCollision(transform.position, _targetPosition, MassApplier.MassToRadius(_mass), condition);
        Vector3 targetPosition;
        if(nearestObstacle == null)
            targetPosition = _targetPosition;
        else
        {
            targetPosition = new Vector3(transform.position.x, transform.position.y, nearestObstacle.transform.position.z);
        }
        StartCoroutine(Fly(targetPosition));
    }

    private float GetExlodingRadius()
    {
        return ExplodeSizeCoef * Mathf.Pow(CurrentRadius, ExplodeSizePow);
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
