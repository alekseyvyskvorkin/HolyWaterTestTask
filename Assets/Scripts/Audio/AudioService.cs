using UnityEngine;
using UnityEngine.Audio;

namespace TestTask.Audio
{
    public class AudioService : MonoBehaviour
    {
        private const string MixerParam = "MusicVolume";

        [SerializeField] private AudioMixer _mixer;
        [SerializeField] private AudioSource _source;

        public void SetVolume(float sliderValue)
        {
            _mixer.SetFloat(MixerParam, Mathf.Log10(sliderValue) * 20);
        }

        public void MuteVolume(bool value)
        {
            _source.mute = value;
        }
    }
}