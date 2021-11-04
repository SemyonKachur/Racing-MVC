using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class CustomButton : Button
{
    public static string ChangeButtonType = nameof(_animationType);
    public static string CurveEase = nameof(_ease);
    public static string Duration = nameof(_duration);

    [SerializeField]
    private AnimationType _animationType = AnimationType.ChangePosition;

    [SerializeField]
    private Ease _ease = Ease.Linear;

    [SerializeField]
    private float _duration;

    private float _strength = 40.0f;
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

    private void ActivateAnimation()
    {
        switch (_animationType)
        {
            case AnimationType.ChangePosition:
                _rectTransform.DOShakeAnchorPos(_duration, _strength).SetEase(_ease);
                break;

            case AnimationType.ChangeRotation:
                _rectTransform.DOShakeRotation(_duration, _strength).SetEase(_ease);
                break;
        }
    }

}
