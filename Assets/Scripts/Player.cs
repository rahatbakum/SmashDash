using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Player : MonoBehaviour
{
    private const string NameOfBulletSlotGameObject = "Bullet Slot";

    [Header("Main Fields")]
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private Door _door;

    [Header("Additional Fields")]
    [SerializeField] private float _startMass = MassApplier.MassOfUnitSphere;
    [SerializeField] private float _minMass = 5f;
    private float _mass;
    private MassApplier _massApplier;
    private BulletShooter _bulletShooter;

    public float Mass
    {
        get => _mass;
        set
        {
            if(value >= _minMass)
            {
                _mass = value;
                _massApplier.ApplyMass(_mass);
            }
            else
            {
                _mass = _minMass;
                _massApplier.ApplyMass(_mass);
                Explode();
                _gameManager.Lose();
            }
        }
    }

    private void Start()
    {
        _massApplier = GetComponentInChildren<MassApplier>();
        _bulletShooter = GetComponentInChildren<BulletShooter>();
        _bulletShooter.Initialize(this, _door, _gameManager);
        Mass = _startMass;
    }

    public void StartLoad()
    {
        _bulletShooter.StartLoad();
    }

    public void Shoot()
    {
        _bulletShooter.Shoot();
    }

    private void Explode()
    {
        Debug.Log("Player Explode");
        Destroy(gameObject);
    }

}


