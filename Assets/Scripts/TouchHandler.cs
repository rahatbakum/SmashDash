using UnityEngine;
using UnityEngine.Events;

public class TouchHandler : MonoBehaviour
{
    public UnityEvent OnTouchDown = new UnityEvent();
    public UnityEvent OnTouchHold = new UnityEvent();
    public UnityEvent OnTouchUp = new UnityEvent();
    
    private bool _previousFrameIsTouched;

    private void Start()
    {
        _previousFrameIsTouched = false;
    }

    private void Update()
    {
        if(Input.touchCount > 0)
        {
            if(!_previousFrameIsTouched)
                OnTouchDown.Invoke();
            OnTouchHold.Invoke();
            _previousFrameIsTouched = true;
        }   
        else
        {
            if(_previousFrameIsTouched)
                OnTouchUp.Invoke();
            _previousFrameIsTouched = false;
        }
    }
}
