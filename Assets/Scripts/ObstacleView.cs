using UnityEngine;

public class ObstacleView : MonoBehaviour
{
    [SerializeField] private Material _explodingMaterial;

    public void ShowStartExploding()
    {
        GetComponent<MeshRenderer>().material = _explodingMaterial;
    }
}
