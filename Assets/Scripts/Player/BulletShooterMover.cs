using UnityEngine;

public class BulletShooterMover : MonoBehaviour
{
    private const float DistanceFromPlayerCoef = 1.5f;

    public void OnPlayerMassChanged(float mass)
    {
        transform.localPosition = new Vector3(0f, 0f, MassApplier.MassToRadius(mass) * DistanceFromPlayerCoef);
    }
}
