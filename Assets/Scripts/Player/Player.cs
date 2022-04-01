using UnityEngine;
using UnityEngine.Events;


public class Player : MonoBehaviour, IExplodingObject, IMassObject
{
    private const string NameOfBulletSlotGameObject = "Bullet Slot";

    [Header("Main Fields")]
    [SerializeField] private Door _door;

    [Header("Additional Fields")]
    [SerializeField] private float _startMass = MassApplier.MassOfUnitSphere;
    [SerializeField] private float _minMass = 5f;
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

    [SerializeField] private UnityEvent<Vector3> _positionChanged;
    public event UnityAction<Vector3> PositionChanged
    {
        add => _positionChanged.AddListener(value);
        remove => _positionChanged.RemoveListener(value);
    }

    [HideInInspector] public PlayerState CurrentPlayerState = PlayerState.Stay;

    private float _mass;
    private BulletShooter _bulletShooter;

    public float Mass
    {
        get => _mass;
        set
        {
            if(value >= _minMass)
            {
                _mass = value;
                _massChanged.Invoke(_mass);
            }
            else
            {
                _mass = _minMass;
                _massChanged.Invoke(_mass);
                Explode();
                GameManager.Instance?.Lose(); 
            }
        }
    }

    public static bool TryGetPlayer(Collider collider, out Player player)
    {
        if(collider.transform?.parent == null)
        {
            player = null;
            return false;
        }
        return collider.transform.parent.TryGetComponent<Player>(out player);
    }

    public float CurrentRadius
    {
        get => MassApplier.MassToRadius(_mass);
    }

    public void SetPosition(Vector3 newPosition)
    {
        transform.position = newPosition;
        _positionChanged.Invoke(transform.position);
    }

    private void Start()
    {
        _bulletShooter = GetComponentInChildren<BulletShooter>();
        _bulletShooter.Initialize(this, _door);
        _positionChanged.Invoke(transform.position);
        Mass = _startMass;
    }

    public void StartLoad()
    {
        if(CurrentPlayerState != PlayerState.Stay)
            return;
        _bulletShooter.StartLoad();
    }

    public void Shoot()
    {
        if(CurrentPlayerState != PlayerState.Stay)
            return;
        _bulletShooter.Shoot();
    }

    public void Explode()
    {
        _exploding.Invoke();
        Debug.Log("Player Explode");
        _exploded.Invoke();
        Destroy(gameObject);
    }

}

public enum PlayerState
{
    Stay,
    Jump
}


