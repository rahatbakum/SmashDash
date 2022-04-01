using UnityEngine;

[RequireComponent (typeof(Player))]
public class TouchPlayerController : MonoBehaviour
{
    [SerializeField]  private TouchHandler _touchHandler;
    private Player _player;

    private void Start()
    {
        _player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        _touchHandler.TouchDown += OnTouchDown;
        _touchHandler.TouchUp += OnTouchUp;
    }

    private void OnDisable()
    {
        _touchHandler.TouchDown -= OnTouchDown;
        _touchHandler.TouchUp -= OnTouchUp;
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
