using UnityEngine;

[RequireComponent (typeof(Road))]
public class RoadController : MonoBehaviour
{

    private const float RadiusToDiameterCoef = 2f;

    [SerializeField] private Player _player;
    [SerializeField] private Transform _door;
    private Road _road;
    
    public void UpdateLengthByPlayerPosition(Vector3 playerPosition)
    {
        _road.UpdateLength(playerPosition, _door.transform.position);
    }

    private void Awake()
    {
        _road = GetComponent<Road>();
        _player.MassChanged += UpdateWidthByPlayerMass;
        _player.PositionChanged += UpdateLengthByPlayerPosition;
    }

    private void UpdateWidthByPlayerMass(float playerMass)
    {
        _road.UpdateWidth(MassApplier.MassToRadius(playerMass) * RadiusToDiameterCoef);
    }
}
