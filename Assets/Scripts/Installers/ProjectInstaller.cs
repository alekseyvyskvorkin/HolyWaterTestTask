using TestTask.Infrastructure;
using UnityEngine;
using Zenject;
using TestTask.Factories;
using TestTask.ScriptableObjects;
using TestTask.Save;

namespace TestTask.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private Config _config;

        public override void InstallBindings()
        {
            Container.Bind<Config>().FromInstance(_config).AsSingle().NonLazy();
            Container.Bind<SaveSystem>().AsSingle();
            Container.Bind<AppStartup>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
            Container.Bind<Factory>().AsSingle().NonLazy();
        }
    }
}