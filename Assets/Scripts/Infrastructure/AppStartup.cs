using UnityEngine;
using Zenject;
using TestTask.Factories;
using TestTask.Save;
using TestTask.UI;
using TestTask.Constants;

namespace TestTask.Infrastructure
{
    public class AppStartup : MonoBehaviour
    {
        private SaveSystem _saveSystem;
        private Factory _factory;

        [Inject]
        private void Initialize(SaveSystem saveSystem, Factory factory)
        {
            _saveSystem = saveSystem;
            _factory = factory;
        }

        private async void Start()
        {
            var levelLoader = await _factory.Create<LevelLoader>(AssetIds.LevelLoaderId);
            if (_saveSystem.LevelData.SceneId == 0)
            {
                await _factory.Create<StartWindow>(AssetIds.StartWindowId);
            }
            else
            {
                levelLoader.LoadLevel(_saveSystem.LevelData.SceneId);
            }
        }
    }
}
