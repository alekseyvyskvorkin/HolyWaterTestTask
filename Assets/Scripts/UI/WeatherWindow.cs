using UnityEngine;
using UnityEngine.UI;

namespace TestTask.UI
{
    public class WeatherWindow : UIWindow
    {
        [field: SerializeField] public Transform WeatherCardsContainer { get; private set; }
        [field: SerializeField] public Button ResetWeatherButton { get; private set; }
    }
}