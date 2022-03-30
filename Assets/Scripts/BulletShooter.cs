using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShooter : MonoBehaviour
{

    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _loadingTime = 3f;
    [SerializeField] private float _startBulletMass = 10f;
    private Player _player;
    private Door _door;
    private BulletShooterState _state = BulletShooterState.NotLoading;
    private bool isInitialized = false;
    private Bullet _bullet;
    private IEnumerator _loadCoroutine;

    private bool RightGameState()
    {
        return GameManager.Instance.MainGameState == GameState.Playing;
    }

    //call this from another object after instantiation
    public void Initialize(Player player, Door door)
    {
        if(isInitialized)
            return;
        _player = player;
        _door = door;
        isInitialized = true;
    }

    private IEnumerator Load()
    {
        if(!RightGameState())
            yield break;
        _bullet = (Instantiate(_bulletPrefab, transform) as GameObject).GetComponent<Bullet>();
        _bullet.Initialize(_door.transform.position, _player.CurrentRadius);
        float startTime = Time.time;
        float startPlayerMass = _player.Mass;

        while(Time.time < startTime + _loadingTime)
        {
            if(!RightGameState())
                yield break;
            float bulletMass = Mathf.Lerp(_startBulletMass, startPlayerMass, (Time.time - startTime) / _loadingTime);
            _player.Mass = startPlayerMass - bulletMass;
            _bullet.Mass = bulletMass;
            yield return null;
        }
    }

    public void StartLoad()
    {
        if(!RightGameState())
            return;
        _state = BulletShooterState.Loading;
        _loadCoroutine = Load();
        StartCoroutine(_loadCoroutine);
    }

    public void Shoot()
    {
        if(!RightGameState())
            return;
        if(_state != BulletShooterState.Loading)
            return;
        _state = BulletShooterState.NotLoading;
        StopCoroutine(_loadCoroutine);
        _bullet.transform.SetParent(GameManager.Instance.PrefabSlot);
        _bullet.Shoot();
    }

    private enum BulletShooterState
    {
        NotLoading,
        Loading
    }
}
