using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using Zenject;

namespace ODckSoundManager.Settings
{
    [CreateAssetMenu(fileName = "AudioClipsInstaller", menuName = "Installers/AudioClipsInstaller")]
    public class ManagerAudioClipsInstaller : ScriptableObjectInstaller<ManagerAudioClipsInstaller>
    {
        public List<ManagerAudioClipsHelper> audioClipsHelper;

        public override void InstallBindings()
        {
            var audioClips = new ManagerAudioClips(audioClipsHelper);
            Container.BindInstance(audioClips).AsSingle();
        }
    }
    
    public class ManagerAudioClips
    {
        public readonly Dictionary<string, AudioClip> clips;

        public ManagerAudioClips(List<ManagerAudioClipsHelper> helper)
        {
            clips = new Dictionary<string, AudioClip>();
            helper.ForEach(x =>
                clips.Add(x.clipName, x.clip)
            );
        }
    }
    
    [Serializable]
    public class ManagerAudioClipsHelper
    {
        public string clipName;
        public AudioClip clip;
    }
}