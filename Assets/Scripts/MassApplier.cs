using UnityEngine;

[RequireComponent (typeof(SphereCollider))]
public class MassApplier : MonoBehaviour
{
    private SphereCollider _sphereCollider;
    private float _startRadius;
    
    private void Start()
    {
        _sphereCollider = GetComponent<SphereCollider>();
        _startRadius = _sphereCollider.radius;
    } 

    private float MassToRadius(float mass) => Mathf.Sqrt(mass);

    private float RadiusToMass(float radius) => Mathf.Pow(radius, 2f);

    private void ApplyRadius(float radius)
    {
        _sphereCollider.radius = radius;
    }

    private void ApplyPosition(float radius)
    {
        transform.position = new Vector3(transform.position.x, radius, transform.position.z);
    }
    public void ApplyMass(float mass)
    {
        float radius = MassToRadius(mass);
        ApplyRadius(radius);
        ApplyPosition(radius);
    }
}
