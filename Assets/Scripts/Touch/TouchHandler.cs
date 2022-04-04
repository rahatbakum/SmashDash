using UnityEngine;
using UnityEngine.Events;

public class TouchHandler : MonoBehaviour
{
    private const int LeftMouseButton = 0;
    private const int RightMouseButton = 1;
    private const int MiddleMouseButton = 2;

    private const int MinTouchCount = 1;

    [SerializeField] private UnityEvent _touchDown = new UnityEvent();
    public event UnityAction TouchDown 
    {
        add => _touchDown.AddListener(value);
        remove => _touchDown.RemoveListener(value);
    }
    [SerializeField] private UnityEvent _touchHold = new UnityEvent();
    public event UnityAction TouchHold 
    {
        add => _touchHold.AddListener(value);
        remove => _touchHold.RemoveListener(value);
    }
    [SerializeField] private UnityEvent _touchUp = new UnityEvent();
    public event UnityAction TouchUp 
    {
        add => _touchUp.AddListener(value);
        remove => _touchUp.RemoveListener(value);
    }
    
    private bool _previousFrameIsTouched;

    private void Start()
    {
        _previousFrameIsTouched = false;
    }

    private void Update()
    {
        if(Input.touchCount >= MinTouchCount || Input.GetMouseButton(LeftMouseButton)) // Input.GetMouseButton(0) added for debug
        {
            if(!_previousFrameIsTouched)
                _touchDown.Invoke();
            _touchHold.Invoke();
            _previousFrameIsTouched = true;
        }   
        else
        {
            if(_previousFrameIsTouched)
                _touchUp.Invoke();
            _previousFrameIsTouched = false;
        }

    }
}
