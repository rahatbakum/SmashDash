using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{
    private const float NecessaryDistanceToNearestObstacle = 1f; 
    private const float WaitAfterObstacleExplodingCoef = 2f;
    private const float RadiusDetectCoef = 0.95f;
    
    [SerializeField] private Player _player;
    [SerializeField] private Transform _playerView;
    [SerializeField] private Transform _door;
    [SerializeField] private AnimationCurve _jumpAnimation;
    [SerializeField] private float _maxJumpDistance = 1.5f;
    [SerializeField] private float _oneJumpTime = 0.5f;
    [SerializeField] private float _jumpHeight = 1.5f;

    private float _jumpsAmount;
    private Vector3 _startPosition;
    private Vector3 _finishPosition;
    private Vector3 nearestObstaclePos = Vector3.zero;

    
    private void SetJumpsAmountAndPositions(Vector3 startPosition, Vector3 finishPosition, float maxJumpDistance)
    {
        Vector3 fixedFinishPosition = new Vector3(finishPosition.x, startPosition.y, finishPosition.z);
        float distance = Vector3.Distance(startPosition, fixedFinishPosition);
        if(distance < maxJumpDistance)
            _jumpsAmount = 0;
        else
            _jumpsAmount = Mathf.CeilToInt(distance / maxJumpDistance);
        _startPosition = startPosition;
        _finishPosition = fixedFinishPosition;
    }

    private void StartMovingDirectlyToTarget(Vector3 targetPosition)
    {
        SetJumpsAmountAndPositions(_playerView.position, targetPosition, _maxJumpDistance);
        if(_jumpsAmount > 0)
            StartCoroutine(Jump());
        else
            StartCoroutine(Crawl());
    }

    private Collider GetNearestObstacle()
    {
        Predicate<Collider> condition = (Collider collider) => Obstacle.TryGetObstacle(collider, out Obstacle obstacle);
        return WayCollisionDetector.GetFirstCollision(_playerView.position, _door.position, MassApplier.MassToRadius(_player.Mass) * RadiusDetectCoef, condition);
    }

    public void StartMovingToDoor()
    {
        
        _player.CurrentPlayerState = PlayerState.Jump;
        StartCoroutine(MoveToDoor(Obstacle.ExplodingTime * WaitAfterObstacleExplodingCoef));
    }

    private IEnumerator MoveToDoor(float timeDelay)
    {
        yield return new WaitForSeconds(timeDelay);
        Collider nearestObstacle = GetNearestObstacle();

        if(nearestObstacle == null)
        {
            nearestObstaclePos = _door.position;
            StartMovingDirectlyToTarget(_door.position);
            yield break;
        }
        nearestObstaclePos = nearestObstacle.transform.position;
        Vector3 direction = (_door.position - _player.transform.position).normalized;
        StartMovingDirectlyToTarget(_player.transform.position + direction * (Vector3.Distance(_player.transform.position, nearestObstacle.transform.position) - NecessaryDistanceToNearestObstacle - MassApplier.MassToRadius(_player.Mass)));
    }

    private Vector2 GetXZ(float jumpProgress)
    {
        Vector3 xyz = Vector3.Lerp(_startPosition, _finishPosition, jumpProgress);
        return new Vector2(xyz.x, xyz.z);
    }

    private float GetY(float jumpProgress)
    {
        return _jumpHeight * _jumpAnimation.Evaluate(jumpProgress);
    }

    private IEnumerator Jump()
    {
        float startTime = Time.time;
        Vector3 startPlayerLocalPosition = _playerView.localPosition;
        while(Time.time - startTime < _oneJumpTime * _jumpsAmount)
        {
            float fullJumpProgress = (Time.time - startTime) / (_oneJumpTime * _jumpsAmount);
            int currentJumpNumber = (int) (fullJumpProgress * _jumpsAmount);
            float currentJumpProgress = fullJumpProgress * _jumpsAmount - currentJumpNumber;
            Vector2 xz = GetXZ(fullJumpProgress);
            float y = GetY(currentJumpProgress);
            _player.SetPosition(new Vector3(xz.x, _player.transform.position.y, xz.y));
            _playerView.localPosition = new Vector3(startPlayerLocalPosition.x, y + startPlayerLocalPosition.y, startPlayerLocalPosition.z);
            yield return null;
        }
        _player.SetPosition(new Vector3(_finishPosition.x, _player.transform.position.y, _finishPosition.z));
        _playerView.localPosition = startPlayerLocalPosition;
        _player.CurrentPlayerState = PlayerState.Stay;
    }
    private IEnumerator Crawl()
    {
        float startTime = Time.time;
        while(Time.time - startTime < _oneJumpTime)
        {
            float fullJumpProgress = (Time.time - startTime) / _oneJumpTime;
            Vector2 xz = GetXZ(fullJumpProgress);
            _player.SetPosition(new Vector3(xz.x, _player.transform.position.y, xz.y));
            yield return null;
        }
        _player.SetPosition(new Vector3(_finishPosition.x, _player.transform.position.y, _finishPosition.z));
        _player.CurrentPlayerState = PlayerState.Stay;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_playerView.position, MassApplier.MassToRadius(_player.Mass));
        Gizmos.DrawWireSphere(nearestObstaclePos, MassApplier.MassToRadius(_player.Mass));
    }

}
