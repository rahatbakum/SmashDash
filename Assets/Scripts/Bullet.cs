using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private const float ExplodeSizeCoefficient = 2f;
    private const float MinDistance = 0.05f;
    private float _mass;
    public float Mass
    {
        get => _mass;
        set
        {
            _mass = value;
            _massApplier.ApplyMass(_mass);
        }
    }
    private float _speed;
    private SphereCollider _sphereCollider;
    private MassApplier _massApplier;
    private Vector3 _targetPosition;
    private Vector3 _previousPosition;


    public void Initialize(Vector3 targetPosition, float startMass = 0f, float speed = 7f)
    {
        _targetPosition = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);
        Mass = startMass;
        _speed = speed;
    }

    private void Awake()
    {
        _previousPosition = transform.position;
        _sphereCollider = GetComponentInChildren<SphereCollider>();
        _massApplier = GetComponentInChildren<MassApplier>();
    }

    private void Move()
    {
        _previousPosition = transform.position;
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime);
    }

    private bool IsTouchedObstacle()
    {
        Collider[] colliders = Physics.OverlapCapsule(_previousPosition, transform.position, _sphereCollider.radius);
        foreach(var item in colliders)
        {
            if(item.TryGetComponent<Obstacle>(out Obstacle obstacle))
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
            Move();
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

    private void Explode()
    {
        Debug.Log("Explode");
        Collider[] colliders = Physics.OverlapSphere(transform.position, _sphereCollider.radius);
        foreach(var item in colliders)
        {
            if(item.TryGetComponent<Obstacle>(out Obstacle obstacle))
            {
                obstacle.Explode();
            }
        }
        Destroy(gameObject);
    }
}
