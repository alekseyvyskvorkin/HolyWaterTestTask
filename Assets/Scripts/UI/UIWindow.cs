using UnityEngine;
using System;
using UnityEngine.UI;
using DG.Tweening;
using TestTask.Enums;

namespace TestTask.UI
{
    public abstract class UIWindow : MonoBehaviour
    {
        public Action<WindowEnum> OnShow;

        [field: SerializeField] public WindowEnum WindowEnum { get; set; }
        [field: SerializeField] public Button CloseButton { get; set; }
        [field: SerializeField] public CanvasGroup CanvasGroup { get; set; }
        [field: SerializeField] public float AnimationTime { get; set; } = 1f;

        private void OnDestroy()
        {
            OnShow = null;
        }

        public virtual void Show()
        {
            OnShow?.Invoke(WindowEnum);
            gameObject.SetActive(true);
            CanvasGroup.alpha = 0f;
            CanvasGroup.DOKill();
            CanvasGroup.DOFade(1, AnimationTime).OnComplete(() => CanvasGroup.interactable = true);
        }

        public virtual void Hide()
        {
            CanvasGroup.interactable = false;
            CanvasGroup.DOKill();
            CanvasGroup.DOFade(0, AnimationTime).OnComplete(() => gameObject.SetActive(false));
        }

        public virtual void ShowImmediate()
        {
            CanvasGroup.alpha = 1.0f;
            gameObject.SetActive(true);
            OnShow?.Invoke(WindowEnum);
        }

        public virtual void HideImmediate()
        {
            gameObject.SetActive(false);
        }

        public void ChangeInteractive(bool value)
        {
            CanvasGroup.interactable = value;
        }
    }
}