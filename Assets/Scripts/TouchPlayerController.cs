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
        _touchHandler.OnTouchUp.AddListener(OnTouchUp);
    }

    private void OnTouchDown()
    {
        _player.StartLoad();
    }

    private void OnTouchUp()
    {
        _player.Shoot();
    }

}
