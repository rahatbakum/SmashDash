using UnityEngine;

[RequireComponent (typeof(SphereCollider))]
public class MassApplier : MonoBehaviour
{
    public const float MassOfUnitSphere = 100f;
    public const float RadiusOfUnitSphere = 0.5f;
    public const float PowOfMass = 1.5f;

    public static float MassToRadius(float mass) => Mathf.Pow(mass / MassOfUnitSphere, 1f / PowOfMass) * RadiusOfUnitSphere;

    public static float RadiusToMass(float radius) => MassOfUnitSphere * Mathf.Pow(radius, PowOfMass) / Mathf.Pow(RadiusOfUnitSphere, PowOfMass);

    public void ApplyMass(float mass)
    {
        float radius = MassToRadius(mass);
        float scale = radius / RadiusOfUnitSphere;
        transform.localScale = new Vector3(scale, scale, scale);
        transform.position = new Vector3(transform.position.x, radius, transform.position.z);
    }
}
