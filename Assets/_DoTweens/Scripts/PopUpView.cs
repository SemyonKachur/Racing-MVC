using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PopUpView : MonoBehaviour
{
    [SerializeField] private Button _buttonClosePopup;
    [SerializeField] private float _duration = 0.3f;
    [SerializeField] public RectTransform _rect;
    public event Action AnimationComplete;

    public void ShowPopup()
    {
        gameObject.SetActive(true);

        AnimationShow();
    }
  
    public void HidePopup()
    {
        AnimationHide();
    }

    private void AnimationShow()
    {
       _rect.transform.localScale = Vector3.zero;
       _rect.DOScale(Vector3.one, _duration);
    }
  
    private void AnimationHide()
    {
        _rect.DOScale(Vector3.zero, _duration).OnComplete(() =>
        {
            gameObject.SetActive(false);
            AnimationComplete?.Invoke();
        });
    }
}
