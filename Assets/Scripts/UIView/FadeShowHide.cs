using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class FadeShowHide : MonoBehaviour
{
    private const float ShowedAlpha = 1f;
    private const float HidedAlpha = 0f;

    [SerializeField] private bool _isShowed = false;
    [SerializeField] private float _animationTime = 1.5f;
    private CanvasGroup _canvasGroup;

    private float AlphaByState(bool isShowed) => isShowed ? ShowedAlpha : HidedAlpha;

    private void ForceApplyState(bool isShowed)
    {
        StopAllCoroutines();
        _canvasGroup.alpha = AlphaByState(isShowed);
        _canvasGroup.interactable = isShowed;
        _canvasGroup.blocksRaycasts = isShowed;
    }

    private void StartApplyingState(bool isShowed)
    {
        StopAllCoroutines();
        StartCoroutine(ApplyState(isShowed));
    }

    private IEnumerator ApplyState(bool isShowed)
    {
        float startTime = Time.time;
        float startAlpha = _canvasGroup.alpha;
        float finishAlpha = AlphaByState(isShowed);
        while(Time.time < startTime + _animationTime)
        {
            _canvasGroup.alpha = Mathf.Lerp(startAlpha, finishAlpha, (Time.time - startTime) / _animationTime);
            yield return null;
        }
        ForceApplyState(isShowed);
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
