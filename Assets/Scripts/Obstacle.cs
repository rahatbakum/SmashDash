using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class Obstacle : MonoBehaviour
{

    private const float ExplodingTime = 0.35f;

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

    public static bool TryGetObstacle(Collider collider, out Obstacle obstacle)
    {
        if(collider.transform?.parent == null)
        {
            obstacle = null;
            return false;
        }
        return collider.transform.parent.TryGetComponent<Obstacle>(out obstacle);
    }

    public void Explode()
    {
        _exploding.Invoke();
        StartCoroutine(FullExplode(ExplodingTime));
    }

    private IEnumerator FullExplode(float time)
    {
        yield return new WaitForSeconds(time);
        _exploded.Invoke();
        Destroy(gameObject);
    }
}
