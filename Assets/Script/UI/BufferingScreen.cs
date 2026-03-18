using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace TD.UI
{
    public class BufferingScreen : BaseView
    {
        [SerializeField] private List<Image> _bufferingImages;
        private float _bufferingDelay = 0.51f;

        private Coroutine _bufferingCoroutine;
        private int _currentIndex = 0;

        public override void ShowView()
        {
            ResetBufferingImages();
            base.ShowView();
            StartBuffering();
        }

        public override void HideView()
        {
            StopBuffering();
            base.HideView();
        }

        private void StartBuffering()
        {
            StopBuffering();
            _bufferingCoroutine = StartCoroutine(BufferingCoroutine());
        }
        
        private void ResetBufferingImages()
        {
            foreach (var image in _bufferingImages)
            {
                image.DOKill();
                image.color = Color.black;
            }
        }

        private void CheckForRestartBuffering()
        {
            if (_currentIndex >= _bufferingImages.Count)
            {
                _currentIndex = 0;
                ResetBufferingImages();
            }
        }

        [Button]
        private void StopBuffering()
        {
            if (_bufferingCoroutine != null)
                StopCoroutine(_bufferingCoroutine);
            _bufferingCoroutine = null;
            _currentIndex = 0;
            ResetBufferingImages();
        }
        
        private IEnumerator BufferingCoroutine()
        {
            while (true)
            {
                Image currentImage = _bufferingImages[_currentIndex];
                currentImage.DOKill();
                currentImage.DOColor(Color.white, 0.5f);
                yield return new WaitForSeconds(_bufferingDelay);
                _currentIndex++;
                CheckForRestartBuffering();
            }
        }
    }
}