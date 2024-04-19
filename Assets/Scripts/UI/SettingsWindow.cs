using TestTask.Audio;
using TestTask.Save;
using UnityEngine;
using UnityEngine.UI;

namespace TestTask.UI
{
    public class SettingsWindow : UIWindow
    {
        [field: SerializeField] public Button OpenSettingsButton { get; private set; }
        [field: SerializeField] public Button ShowModalScreenButton { get; private set; }
        [field: SerializeField] public Slider MusicVolumeSlider { get; private set; }
        [field: SerializeField] public Toggle VolumeToggle { get; private set; }

        public override void Show()
        {
            base.Show();
        }

        public override void Hide()
        {
            base.Hide();
        }

        public void ChangeVolume(SaveSystem saveSystem, AudioService audioService, float value)
        {
            MusicVolumeSlider.value = value;
            audioService.SetVolume(value);
            saveSystem.SettingsData.MusicVolume = value;
            saveSystem.SaveSettingsData();
        }

        public void MuteVolume(SaveSystem saveSystem, AudioService audioService, bool value)
        {
            VolumeToggle.isOn = value;
            audioService.MuteVolume(!value);
            saveSystem.SettingsData.HasVolume = value;
            saveSystem.SaveSettingsData();
        }
    }
}