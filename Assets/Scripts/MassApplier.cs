using UnityEngine;

[RequireComponent (typeof(SphereCollider))]
public class MassApplier : MonoBehaviour
{
    private SphereCollider _sphereCollider;
    private float _startColliderRadius;
    private Vector3 _startScale;
    
    private void Start()
    {
        _sphereCollider = GetComponent<SphereCollider>();
        _startColliderRadius = _sphereCollider.radius;
        _startScale = transform.localScale;
    } 

    private float MassToRadius(float mass) => Mathf.Sqrt(mass);

    private float RadiusToMass(float radius) => Mathf.Pow(radius, 2f);

    private void ApplyCollider(float radius)
    {
        _sphereCollider.radius = radius * _startColliderRadius;
    }

    private void ApplyTransform(float radius)
    {
        transform.localScale = radius * _startScale;
        transform.position = new Vector3(transform.position.x, radius * _startColliderRadius * _startScale.y, transform.position.z);
    }
    public void ApplyMass(float mass)
    {
        float radius = MassToRadius(mass);
        ApplyCollider(radius);
        ApplyTransform(radius);
    }
}
