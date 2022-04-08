using UnityEngine;

public class ObstacleView : MonoBehaviour
{

    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private Material _explodingMaterial;

    public void ShowStartExploding()
    {
        _meshRenderer.material = _explodingMaterial;
    }
}
