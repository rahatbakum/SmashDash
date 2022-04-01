using UnityEngine;

public class ParticleEffectController : MonoBehaviour
{
    [SerializeField] private bool _playOnAwake = true;
    [SerializeField] private bool _destroyAfterEffect = true;
    private ParticleSystem[] _particleSystems;
    private bool _isStarted = false;

    private void GetAllParticleSystems()
    {
        _particleSystems = GetComponentsInChildren<ParticleSystem>();
    }

    public void StartAllParticleSystems()
    {
        if(_isStarted)
            return;
        
        _isStarted = true;
        GetAllParticleSystems();
        float maxDuration = 0f;
        foreach(var item in _particleSystems)
        {
            if(item.main.duration > maxDuration)
                maxDuration = item.main.duration;
            item.Play();
        }
        if(_destroyAfterEffect)
            Destroy(gameObject, maxDuration);
    }

    private void Awake()
    {
        if(_playOnAwake)
            StartAllParticleSystems();
    }

}
