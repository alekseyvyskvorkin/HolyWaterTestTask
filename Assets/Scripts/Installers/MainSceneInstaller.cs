using TestTask.Audio;
using TestTask.UI;
using TestTask.Weather;
using UnityEngine;
using Zenject;

namespace TestTask.Installers
{
    public class MainSceneInstaller : MonoInstaller
    {
        [SerializeField] private WindowController _windowController;
        [SerializeField] private AudioService _audioService;
        [SerializeField] private WeatherCardsCreator _weatherCardsCreator;
        [SerializeField] private WeatherWindow _weatherWindow;

        public override void InstallBindings()
        {
            Container.Bind<WeatherWindow>().FromInstance(_weatherWindow).AsSingle().NonLazy();
            Container.Bind<WeatherCardsCreator>().FromInstance(_weatherCardsCreator).AsSingle().NonLazy();
            Container.Bind<WindowController>().FromInstance(_windowController).AsSingle().NonLazy();
            Container.Bind<AudioService>().FromInstance(_audioService).AsSingle().NonLazy();
        }
    }
}