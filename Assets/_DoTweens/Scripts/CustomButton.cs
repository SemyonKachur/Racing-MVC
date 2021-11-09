using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Profile;
using UnityEngine.EventSystems;

public class CustomButton : Button
{
    public static string ChangeButtonType = nameof(animationType);
    public static string CurveEase = nameof(ease);
    public static string Duration = nameof(duration);
    public static string GameState = nameof(gameState);
    internal event Action<GameState> AnimationEnd;  
    
    [SerializeField] private GameState gameState;
    [SerializeField] private AnimationType animationType = AnimationType.ChangePosition;
    [SerializeField] private Ease ease = Ease.Linear;
    [SerializeField] private float duration;
    

    private float _strength = 40.0f;
    private readonly Vector3 _endValue = new Vector3(1.2f, 1.2f, 1.2f);
    private RectTransform _rectTransform;

    protected override void Awake()
    {
        base.Awake();

        _rectTransform = GetComponent<RectTransform>();
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);

        ActivateAnimation();
    }

    public void ActivateAnimation()
    {
        switch (animationType)
        {
            case AnimationType.ChangePosition:
                _rectTransform.DOShakeAnchorPos(duration, _strength).SetEase(ease).OnComplete(()=> AnimationEnd?.Invoke(gameState));
                break;
            case AnimationType.ChangeRotation:
                _rectTransform.DOShakeRotation(duration, _strength).SetEase(ease).OnComplete(()=> AnimationEnd?.Invoke(gameState));
                break;
            case AnimationType.ChangeScale:
                var sequence = DOTween.Sequence();
                sequence.Insert(0.0f, _rectTransform.DOScale(_endValue, duration));
                sequence.OnComplete(() =>
                {
                    _rectTransform.DOScale(Vector3.one, duration).OnComplete(()=>
                    {
                        sequence = null;
                        AnimationEnd?.Invoke(gameState);
                    });
                });
                break;
        }
    }

}
