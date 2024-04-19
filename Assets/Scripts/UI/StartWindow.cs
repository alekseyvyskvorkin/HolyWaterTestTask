using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace TestTask.UI
{
    public class StartWindow : MonoBehaviour
    {
        [SerializeField] private Button _loadMainSceneButton;

        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _animationTime = 2f;

        private LevelLoader _levelLoader;

        [Inject]
        private void Initialize(LevelLoader levelLoader)
        {
            _levelLoader = levelLoader;
        }

        private void Start()
        {
            _canvasGroup.DOFade(1, _animationTime);
            _loadMainSceneButton.onClick.AddListener(() =>
            {
                _loadMainSceneButton.interactable = false;
                _levelLoader.LoadLevel(1);
            });
        }
    }
}