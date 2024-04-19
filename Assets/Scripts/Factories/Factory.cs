using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;
using UnityEngine.AddressableAssets;

namespace TestTask.Factories
{
    public class Factory
    {
        private DiContainer _diContainer;

        public Factory(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public async Task<T> Create<T>(string assetId) where T : MonoBehaviour
        {
            var handle = Addressables.InstantiateAsync(assetId);
            var createdObject = await handle.Task;
            createdObject.TryGetComponent<T>(out var component);

            _diContainer.Bind<T>().FromInstance(component).NonLazy();
            _diContainer.Inject(component);

            return component;
        }
    }
}
