using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class FadeShowHide : MonoBehaviour
{
    [SerializeField] private bool _isShowed = false;
    [SerializeField] private float _animationTime = 1.5f;
    private CanvasGroup _canvasGroup;

    private float AlphaByState(bool state)
    {
        if(state)
            return 1f;
        return 0f;
    }

    private void ForceApplyState(bool state)
    {
        StopAllCoroutines();
        _canvasGroup.alpha = AlphaByState(state);
        _canvasGroup.interactable = state;
        _canvasGroup.blocksRaycasts = state;
    }

    private void StartApplyingState(bool state)
    {
        StopAllCoroutines();
        StartCoroutine(ApplyState(state));
    }

    private IEnumerator ApplyState(bool state)
    {
        float startTime = Time.time;
        float startAlpha = _canvasGroup.alpha;
        float finishAlpha = AlphaByState(state);
        while(Time.time < startTime + _animationTime)
        {
            _canvasGroup.alpha = Mathf.Lerp(startAlpha, finishAlpha, (Time.time - startTime) / _animationTime);
            yield return null;
        }
        ForceApplyState(state);
    }

    public void Show()
    {
        _isShowed = true;
        StartApplyingState(_isShowed);
    }

    
    public void Hide()
    {
        _isShowed = false;
        StartApplyingState(_isShowed);
    }

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        ForceApplyState(_isShowed);
    }
}
