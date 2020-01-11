using System;
using ODckSoundManager.Settings;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace ODckSoundManager.SoundManagerZenject
{
    public class SettingsMenu : MonoBehaviour
    {
        [Inject] private SoundSettings _settings;

        [SerializeField] private Slider masterVolume;
        [SerializeField] private Slider musicVolume;
        [SerializeField] private Slider sfxVolume;
        [SerializeField] private Toggle mute;

        private void Start()
        {
            SetUpVariables();
            masterVolume.onValueChanged.AddListener(x => _settings.MasterVolume = x);
            musicVolume.onValueChanged.AddListener(x => _settings.MusicVolume = x);
            sfxVolume.onValueChanged.AddListener(x => _settings.SfxVolume = x);
            mute.onValueChanged.AddListener(x => _settings.MuteAll = x);
        }

        private void OnDestroy()
        {
            masterVolume.onValueChanged.RemoveAllListeners();
            musicVolume.onValueChanged.RemoveAllListeners();
            sfxVolume.onValueChanged.RemoveAllListeners();
            mute.onValueChanged.RemoveAllListeners();
        }

        private void SetUpVariables()
        {
            masterVolume.value = _settings.MasterVolume;
            musicVolume.value = _settings.MusicVolume;
            sfxVolume.value = _settings.SfxVolume;
            mute.isOn = _settings.MuteAll;
        }
    }
}