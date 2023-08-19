using System;
using System.Collections;
using System.Collections.Generic;
using GlideGame.Interfaces;
using GlideGame.Utils;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace GlideGame.UI.Screens
{
    public class FailScreen : MonoBehaviour, IObserver
    {
        [SerializeField] private HandleButton handleButton;
        public HandleButton HandleButton => handleButton;
        [SerializeField] private RectTransform panelTransform;
        [SerializeField] private Ease animationEase;
        [SerializeField] private float animationDuration;
        public Action FailCallback;
        protected Vector3 initScale = Vector3.zero, finalScale = Vector3.one;
        public void Show()
        {
            gameObject.SetActive(true);
            panelTransform.AnimatePanel(initScale, finalScale, animationDuration, animationEase);

        }
        public void Hide() { gameObject.SetActive(false); }
        private void Start()
        {
            HandleButton.Attach(this);
        }
        private void OnDestroy()
        {
            HandleButton.Detach(this);
        }

        public void UpdateObserver(IObservable observable)
        {
            Hide();
            FailCallback?.Invoke();
        }
    }
}
