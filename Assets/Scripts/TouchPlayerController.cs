using UnityEngine;

[RequireComponent (typeof(TouchHandler))]
public class TouchPlayerController : MonoBehaviour
{
    [SerializeField] private Player _player;
    private TouchHandler _touchHandler;

    private void Start()
    {
        _touchHandler = GetComponent<TouchHandler>();
        _touchHandler.OnTouchDown.AddListener(OnTouchDown);
        _touchHandler.OnTouchDown.AddListener(OnTouchUp);
    }

    private void OnTouchDown()
    {
        _player.Load();
    }

    private void OnTouchUp()
    {
        _player.Shoot();
    }

}
