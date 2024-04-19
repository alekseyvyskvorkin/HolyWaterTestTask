using UnityEngine;

namespace TestTask.UI
{
    public class ModalWindow : UIWindow
    {
        [SerializeField] private CanvasGroup[] _blockedCanvases;

        /// <summary>
        /// Invokes from unity buttons
        /// </summary>
        /// <param name="url"></param>
        public void OpenUrl(string url)
        {
            Application.OpenURL(url);
        }

        public override void Hide()
        {
            base.Hide();
            UnBlockCanvases(true);
        }

        public override void Show()
        {
            base.Show();
            UnBlockCanvases(false);
        }

        public override void ShowImmediate()
        {
            base.ShowImmediate();
            UnBlockCanvases(false);
        }

        private void UnBlockCanvases(bool value)
        {
            foreach (var canvas in _blockedCanvases)
            {
                canvas.interactable = value;
                canvas.blocksRaycasts = value;
            }
        }
    }
}