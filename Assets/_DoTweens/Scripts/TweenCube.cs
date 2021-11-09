using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class TweenCube : MonoBehaviour
{
    [SerializeField]
    private float _duration;

    [SerializeField]
    private int _countLooping;

    [SerializeField]
    private float _endPositionX;

    [SerializeField]
    private float _endScale;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
            ComplexTweens();
    }

    private void ComplexTweens()
    {
        var sequence = DOTween.Sequence();

        sequence.Append(transform.DOMoveX(_endPositionX, _duration));
        sequence.Insert(0, transform.DOScale(_endScale, _duration));
        sequence.Insert(3, transform.DOJump(Vector3.right, 3, 4, _duration));
    }
}
