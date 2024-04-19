using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;
using TestTask.Save;
using TestTask.ScriptableObjects;
using TestTask.Factories;
using TestTask.UI;
using TestTask.Constants;

namespace TestTask.Weather
{
    public class WeatherCardsCreator : MonoBehaviour
    {
        private const string ApiKey = "b2fd4a314c545ed5f1406bc8c9f0e8c3";

        private List<WeatherCard> _cards { get; set; } = new List<WeatherCard>();
        private Transform _cardContainer { get; set; }

        private SaveSystem _saveSystem;
        private Factory _factory;
        private Config _config;

        [Inject]
        public void Initialize(Factory factory, SaveSystem saveSystem, Config config, WeatherWindow weatherWindow)
        {
            _factory = factory;
            _saveSystem = saveSystem;
            _config = config;
            _cardContainer = weatherWindow.WeatherCardsContainer;
        }

        private void Start()
        {
            CreateWeatherCards();
        }

        public void ResetCards()
        {
            _saveSystem.LevelData.Cities.Clear();
            _saveSystem.LevelData.Cities.AddRange(_config.Cities);
            _saveSystem.SaveLevelData();
            CreateWeatherCards();
        }

        public async Task CreateWeatherCards()
        {
            foreach (var card in _cards)
            {
                if (card != null)
                    Destroy(card.gameObject);
            }
            _cards.Clear();

            foreach (var city in _saveSystem.LevelData.Cities)
            {
                await LoadWeather(city);
            }
        }

        private async Task LoadWeather(string cityName)
        {
            string url = "api.openweathermap.org/data/2.5/weather?" + "q=" + cityName + "&appid=" + ApiKey;
            UnityWebRequest webRequest = UnityWebRequest.Get(url);
            await webRequest.SendWebRequest();
            if (webRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log("Web request error: " + webRequest.error);
            }
            else
            {
                await CreateWeatherStatus(webRequest.downloadHandler.text, cityName);
            }
        }

        private async Task CreateWeatherStatus(string json, string cityName)
        {
            WeatherStatus weatherStatus = new WeatherStatus();

            dynamic jsonData = JObject.Parse(json);
            weatherStatus.City = cityName;
            weatherStatus.Temperature = jsonData.main.temp - 273.15f; //convert to Celsius
            var icon = jsonData.weather[0].icon;
            string url = "openweathermap.org/img/wn/" + icon + ".png";
            await LoadIcon(url, weatherStatus);
        }

        private async Task LoadIcon(string url, WeatherStatus weatherStatus)
        {
            using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(url))
            {
                await webRequest.SendWebRequest();
                if (webRequest.isNetworkError || webRequest.isHttpError)
                {
                    Debug.Log("Web request error: " + webRequest.error);
                }
                else
                {
                    Texture2D texture = ((DownloadHandlerTexture)webRequest.downloadHandler).texture;
                    weatherStatus.Icon = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                    var cardTask = _factory.Create<WeatherCard>(AssetIds.WeatherCardId);
                    await cardTask;
                    var card = cardTask.Result;
                    card.Initialize(weatherStatus, _saveSystem, _cardContainer);
                    _cards.Add(card);
                }
            }
        }
    }
}
