using UnityEngine;

public class ParticleSystemStarter : MonoBehaviour
{
    [SerializeField] GameObject _effectPrefab;

    public void StartEffect()
    {
        if(GameManager.Instance != null)
            Instantiate(_effectPrefab, transform.position, transform.rotation, GameManager.Instance.PrefabSlot);
        else
            Instantiate(_effectPrefab, transform.position, transform.rotation);
    }
}
