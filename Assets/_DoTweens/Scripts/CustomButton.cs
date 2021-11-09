using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Profile;
using UnityEngine.EventSystems;

public class CustomButton : Button
{
    public static string ChangeButtonType = nameof(_animationType);
    public static string CurveEase = nameof(_ease);
    public static string Duration = nameof(_duration);
    public static string GameState = nameof(_gameState);
    internal event Action<GameState> _animationEnd;  
    
    [SerializeField] private GameState _gameState;
    [SerializeField] private AnimationType _animationType = AnimationType.ChangePosition;
    [SerializeField] private Ease _ease = Ease.Linear;
    [SerializeField] private float _duration;
    

    private float _strength = 40.0f;
    private Vector3 _endValue = new Vector3(1.2f, 1.2f, 1.2f);
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
        switch (_animationType)
        {
            case AnimationType.ChangePosition:
                _rectTransform.DOShakeAnchorPos(_duration, _strength).SetEase(_ease).OnComplete(()=> _animationEnd?.Invoke(_gameState));
                break;
            case AnimationType.ChangeRotation:
                _rectTransform.DOShakeRotation(_duration, _strength).SetEase(_ease).OnComplete(()=> _animationEnd?.Invoke(_gameState));
                break;
            case AnimationType.ChangeScale:
                var sequence = DOTween.Sequence();
                sequence.Insert(0.0f, _rectTransform.DOScale(_endValue, _duration));
                sequence.OnComplete(() =>
                {
                    _rectTransform.DOScale(Vector3.one, _duration).OnComplete(()=>
                    {
                        sequence = null;
                        _animationEnd?.Invoke(_gameState);
                    });
                });
                break;
        }
    }

}
