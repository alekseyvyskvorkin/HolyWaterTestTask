using TestTask.Save;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TestTask.Weather
{
    public class WeatherCard : MonoBehaviour
    {
        private const string CelsiusUnicode = "\u00B0";

        [SerializeField] private Image _weatherIcon;
        [SerializeField] private TMP_Text _cityName;
        [SerializeField] private TMP_Text _temperature;

        private Button _button;
        private SaveSystem _saveSystem;

        public void Initialize(WeatherStatus weatherStatus, SaveSystem saveSystem, Transform parent)
        {
            _button = GetComponent<Button>();
            _saveSystem = saveSystem;

            _button.onClick.AddListener(() =>
            {
                _saveSystem.LevelData.Cities.Remove(weatherStatus.City);
                _saveSystem.SaveLevelData();
                Destroy(gameObject);
            });

            _weatherIcon.sprite = weatherStatus.Icon;
            _cityName.text = weatherStatus.City;
            _temperature.text = ((int)weatherStatus.Temperature).ToString() + CelsiusUnicode;
            transform.SetParent(parent);
        }
    }
}