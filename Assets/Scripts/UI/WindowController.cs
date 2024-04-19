using TestTask.Audio;
using TestTask.Enums;
using TestTask.Save;
using TestTask.Weather;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace TestTask.UI
{
    public class WindowController : MonoBehaviour
    {
        [SerializeField] private UIWindow[] _allUIWindows;
        [SerializeField] private SettingsWindow _settingsWindow;
        [SerializeField] private ModalWindow _modalWindow;

        [SerializeField] private Button _quitButton;

        private WeatherWindow _weatherWindow;

        private AudioService _audioService;
        private SaveSystem _saveSystem;
        private WeatherCardsCreator _weatherCardsCreator;

        [Inject]
        private void Initialize(SaveSystem saveSystem,
            AudioService audioService,
            WeatherCardsCreator weatherCardsCreator,
            WeatherWindow weatherWindow)
        {
            _saveSystem = saveSystem;
            _audioService = audioService;
            _weatherCardsCreator = weatherCardsCreator;
            _weatherWindow = weatherWindow;
        }

        private void Start()
        {
            _quitButton.onClick.AddListener(() => Application.Quit());
            InitializeSettingsWindow();
            InitializeModalWindow();
            InitializeWeatherWindow();
            ShowWindowImmediate(_saveSystem.LevelData.WindowEnum);
        }

        private void ShowWindowImmediate(WindowEnum windowEnum)
        {
            foreach (var window in _allUIWindows)
            {
                window.ChangeInteractive(window.WindowEnum == windowEnum);
                if (window.WindowEnum == windowEnum)
                {
                    window.ShowImmediate();
                }
                window.OnShow += SaveLevelData;
            }
        }

        private void SaveLevelData(WindowEnum windowEnum)
        {
            _saveSystem.LevelData.WindowEnum = windowEnum;
            _saveSystem.SaveLevelData();
        }

        private void ChangeWindowInteractive(WindowEnum windowEnum)
        {
            foreach (var window in _allUIWindows)
            {
                window.ChangeInteractive(window.WindowEnum == windowEnum);
            }
        }

        private void InitializeSettingsWindow()
        {
            _settingsWindow.OnShow += ChangeWindowInteractive;
            _settingsWindow.OpenSettingsButton.onClick.AddListener(() =>
            {
                _weatherWindow.Hide();
                _settingsWindow.Show();
            });
            _settingsWindow.CloseButton.onClick.AddListener(() =>
            {
                _settingsWindow.Hide();
                _weatherWindow.Show();
            });
            _settingsWindow.ShowModalScreenButton.onClick.AddListener(() =>
            {
                _settingsWindow.Hide();
                _modalWindow.Show();
            });
            _settingsWindow.MusicVolumeSlider.onValueChanged.AddListener((value) =>
            {
                _settingsWindow.ChangeVolume(_saveSystem, _audioService, value);
            });
            _settingsWindow.VolumeToggle.onValueChanged.AddListener((value) =>
            {
                _settingsWindow.MuteVolume(_saveSystem, _audioService, value);
            });

            _settingsWindow.MusicVolumeSlider.onValueChanged.Invoke(_saveSystem.SettingsData.MusicVolume);
            _settingsWindow.VolumeToggle.onValueChanged.Invoke(_saveSystem.SettingsData.HasVolume);
        }

        private void InitializeModalWindow()
        {
            _modalWindow.OnShow += ChangeWindowInteractive;

            _modalWindow.CloseButton.onClick.AddListener(() =>
            {
                _weatherWindow.Show();
                _modalWindow.Hide();
            });
        }

        private void InitializeWeatherWindow()
        {
            _weatherWindow.ResetWeatherButton.onClick.AddListener(() =>
            {
                _weatherWindow.HideImmediate();
                _settingsWindow.HideImmediate();
                _modalWindow.Show();
                _weatherCardsCreator.ResetCards();
            });
        }
    }
}