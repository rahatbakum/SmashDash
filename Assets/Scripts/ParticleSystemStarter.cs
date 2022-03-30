using UnityEngine;

public class ParticleSystemStarter : MonoBehaviour
{
    [SerializeField] GameObject _effectPrefab;

    public void StartEffect()
    {
        Instantiate(_effectPrefab, transform.position, transform.rotation, GameManager.Instance.PrefabSlot);
    }
}
