using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;
using DG.Tweening;
using UnityEngine.AddressableAssets;
using TestTask.Save;
using TestTask.ScriptableObjects;

namespace TestTask.UI
{
    public class LevelLoader : MonoBehaviour
    {
        [SerializeField] private float _hideCanvasTime = 2f;
        [SerializeField] private CanvasGroup _canvasGroup;

        [SerializeField] private GameObject _progressBarParent;
        [SerializeField] private Image _progressBar;
        [SerializeField] private TMP_Text _progressText;

        private SaveSystem _saveSystem;
        private Config _config;

        [Inject]
        private void Initialize(SaveSystem saveSystem, Config config)
        {
            _saveSystem = saveSystem;
            _config = config;
            DontDestroyOnLoad(gameObject);
        }

        public void LoadLevel(int index)
        {
            if (_saveSystem.LevelData.SceneId == 0)
                _saveSystem.LevelData.Cities.AddRange(_config.Cities);
            _saveSystem.LevelData.SceneId = index;
            _saveSystem.SaveLevelData();

            StartCoroutine(LoadSceneAsync(index));
        }

        private IEnumerator LoadSceneAsync(int index)
        {
            _progressBarParent.SetActive(true);
            _progressBar.fillAmount = 0;
            _progressText.text = "0%";

            AsyncOperation operation = SceneManager.LoadSceneAsync(index);
            operation.allowSceneActivation = false;

            float progress = 0;

            while (!operation.isDone)
            {
                progress = Mathf.MoveTowards(progress, operation.progress, Time.deltaTime);

                _progressBar.fillAmount = progress;
                _progressText.text = ((int)(progress * 100)).ToString() + "%";

                if (progress >= 0.9f)
                {
                    operation.allowSceneActivation = true;
                    _progressBar.fillAmount = 1;
                    _progressText.text = "100%";
                }

                yield return null;
            }

            _canvasGroup.DOFade(0, _hideCanvasTime).OnComplete(() => Addressables.ReleaseInstance(gameObject));
        }
    }
}