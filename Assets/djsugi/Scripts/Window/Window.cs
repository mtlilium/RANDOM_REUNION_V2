using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UniRx;
using NaughtyAttributes;
using System.Linq;
using System;
using DS.Util;
using DS.UI.WindowAnimation;
using DG.Tweening;

namespace DS.UI
{
    public class Window : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private bool shouldInactiveOnPlay = true;
        [SerializeField] private bool shouldPauseOnOpen = false;

        [Header("Events"), SerializeField]
        private bool enableEvents;
        [ShowIf("enableEvents")]
        public UnityEvent onOpen = new UnityEvent();
        [ShowIf("enableEvents")]
        public UnityEvent onClose = new UnityEvent();

        [Header("Animation")]
        [ReorderableList]
        public List<UIAnimation> animations = new List<UIAnimation>();


        private void Awake()
        {
            if (shouldInactiveOnPlay) gameObject.SetActive(false);
        }

        public void Open()
        {
            shouldInactiveOnPlay = false;
            gameObject.SetActive(true);
            if (shouldPauseOnOpen) TimeScaleManager.Instance.Pause();

            ProcessAnimations(
                animations.Select(a => a.Show()), 
                () => {
                    onOpen.Invoke();
                });
        }

        public void Close()
        {

            ProcessAnimations(
                animations.Select(a => a.Hide()), 
                () => {
                    if (shouldPauseOnOpen) TimeScaleManager.Instance.Restart();
                    onClose.Invoke();
                    gameObject.SetActive(false);
                });
        }

        public void Toggle()
        {
            if (gameObject.activeSelf) Close();
            else Open();
        }


        private void ProcessAnimations(IEnumerable<Tween> tweens, TweenCallback callback)
        {
            if (animations.Count() == 0)
            {
                callback();
                return;
            }

            var sequence = DOTween.Sequence();
            foreach (var tween in tweens)
            {
                sequence.Join(tween);
            }

            sequence
                .OnComplete(callback)
                .SetLink(gameObject)
                .SetUpdate(true);
        }

#if UNITY_EDITOR
        [Button]
        public void SearchAnimations()
        {
            animations =  GetComponentsInChildren<UIAnimation>().ToList();
        }
#endif

    }
}
