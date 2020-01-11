using System.IO;
using UniRx;
using UnityEngine;

namespace ODckSoundManager.Settings
{
    public class SoundSettings
    {
        private class Configuration
        {
            public float masterVolume = 1;
            public float musicVolume = 1;
            public float sfxVolume = 1;
            public bool muteAll = false;
        }

        private readonly Configuration _configuration;

        public readonly Subject<bool> onConfigurationChanged;

        #region Properties

        public float MasterVolume
        {
            get => _configuration.masterVolume;
            set
            {
                _configuration.masterVolume = value;
                SaveSoundSettings();
            }
        }

        public float MusicVolume
        {
            get => _configuration.musicVolume;
            set
            {
                _configuration.musicVolume = value;
                SaveSoundSettings();
            }
        }

        public float SfxVolume
        {
            get => _configuration.sfxVolume;
            set
            {
                _configuration.sfxVolume = value;
                SaveSoundSettings();
            }
        }

        public bool MuteAll
        {
            get => _configuration.muteAll;
            set
            {
                _configuration.muteAll = value;
                SaveSoundSettings();
            }
        }

        #endregion


        public SoundSettings()
        {
            var savePath = Application.persistentDataPath + "/soundSettings.json";
            onConfigurationChanged = new Subject<bool>();

            if (File.Exists(savePath))
            {
                using (var reader = new StreamReader(savePath))
                {
                    var json = reader.ReadToEnd();
                    _configuration = JsonUtility.FromJson<Configuration>(json);
                }
            }
            else
            {
                _configuration = new Configuration();
            }
        }

        private void SaveSoundSettings()
        {
            var savePath = Application.persistentDataPath + "/soundSettings.json";

            using (var writer = new StreamWriter(savePath))
            {
                var json = JsonUtility.ToJson(_configuration);
                writer.Write(json);
                onConfigurationChanged.OnNext(true);
            }
        }
    }
}