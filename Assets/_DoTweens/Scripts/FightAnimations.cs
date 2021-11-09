using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace DoTweens
{
    internal class FightAnimations
    {
        private readonly FightWindowView _fightView;

        private Vector3 _playerWeelRotation = new Vector3(0, 0, -360);
        private Vector3 _playerMoveBackPosition = new Vector3(-600, 0);
        private Vector3 _playerMoveForwardPosition = new Vector3(-270, 0);
        
        private Vector3 _enemyWeelRotation = new Vector3(0, 0, 360);
        private Vector3 _enemyMoveBackPosition = new Vector3(600, 0);
        private Vector3 _enemyMoveForwardPosition = new Vector3(270, 0);
        
        private Vector3 _playerVictoryUpPos = new Vector3(-270,320);
        private Vector3 _playerVictoryDownPos = new Vector3(-270,150);
        private Vector3 _playerLooseDownPos = new Vector3(-270, 0);
        
        private Vector3 _enemyLooseUpPos = new Vector3(270, 320);
        private Vector3 _enemyWinDownPos = new Vector3(270, 150);
        private Vector3 _enemyLooseDownPos = new Vector3(270, 0);

        public event Action Winner;  

        public FightAnimations(FightWindowView fightView)
        {
            _fightView = fightView;
        }
        
        public void FightAnimation()
        {
            _fightView.Winner.gameObject.SetActive(false);
            _fightView.Looser.gameObject.SetActive(false);
            
            List<Image> Playerimage = new List<Image>(_fightView.Player.GetComponentsInChildren<Image>());
            Playerimage[1].rectTransform.DORotate(_playerWeelRotation, 0.5f, RotateMode.FastBeyond360).SetLoops(-1,LoopType.Restart).SetEase(Ease.Linear);
            Playerimage[2].rectTransform.DORotate(_playerWeelRotation, 0.5f,RotateMode.FastBeyond360).SetLoops(-1,LoopType.Restart).SetEase(Ease.Linear);
            
            List<Image> EnemyImage = new List<Image>(_fightView.Enemy.GetComponentsInChildren<Image>());
            EnemyImage[1].rectTransform.DORotate(_enemyWeelRotation, 0.5f, RotateMode.FastBeyond360).SetLoops(-1,LoopType.Restart).SetEase(Ease.Linear);
            EnemyImage[2].rectTransform.DORotate(_enemyWeelRotation, 0.5f,RotateMode.FastBeyond360).SetLoops(-1,LoopType.Restart).SetEase(Ease.Linear);

            var playerSequence = DOTween.Sequence();
            var enemySequence = DOTween.Sequence();

            playerSequence.Append(_fightView.Player.rectTransform.DOLocalMove(_playerMoveBackPosition, 1.0f).SetEase(Ease.Linear));
            playerSequence.Append(_fightView.Player.rectTransform.DOLocalMove(_playerMoveForwardPosition, 0.5f).SetEase(Ease.OutBounce)).OnComplete(()=>Winner?.Invoke());

            enemySequence.Append(_fightView.Enemy.rectTransform.DOLocalMove(_enemyMoveBackPosition, 1.0f).SetEase(Ease.Linear));
            enemySequence.Append(_fightView.Enemy.rectTransform.DOLocalMove(_enemyMoveForwardPosition, 0.5f).SetEase(Ease.OutBounce));
        }

        public void PlayerWinAnimation()
        {
            _fightView.Winner.gameObject.SetActive(true);
            _fightView.Winner.gameObject.GetComponent<RectTransform>().anchoredPosition = _playerVictoryUpPos;
            
            _fightView.Looser.gameObject.SetActive(true);
            _fightView.Looser.gameObject.GetComponent<RectTransform>().anchoredPosition = _enemyLooseUpPos;
            
            _fightView.Winner.rectTransform.DOLocalMove(_playerVictoryDownPos, 1.0f).SetEase(Ease.Linear);
            _fightView.Looser.rectTransform.DOLocalMove(_enemyLooseDownPos, 1.0f).SetEase(Ease.Linear);
        }

        public void PlayerLooseAnimation()
        {
            _fightView.Winner.gameObject.SetActive(true);
            _fightView.Winner.gameObject.GetComponent<RectTransform>().anchoredPosition = _enemyLooseUpPos;
            
            _fightView.Looser.gameObject.SetActive(true);
            _fightView.Looser.gameObject.GetComponent<RectTransform>().anchoredPosition = _playerVictoryUpPos;
            
            _fightView.Winner.rectTransform.DOLocalMove(_enemyWinDownPos, 1.0f).SetEase(Ease.Linear);
            _fightView.Looser.rectTransform.DOLocalMove(_playerLooseDownPos, 1.0f).SetEase(Ease.Linear);
        }
    }
}