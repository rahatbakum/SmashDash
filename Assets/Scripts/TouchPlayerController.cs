using UnityEngine;

[RequireComponent (typeof(Player))]
public class TouchPlayerController : MonoBehaviour
{
    [SerializeField]  private TouchHandler _touchHandler;
    private Player _player;

    private void Start()
    {
        _player = GetComponent<Player>();
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
