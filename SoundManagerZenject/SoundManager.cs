using System;
using ODckSoundManager.Settings;
using UniRx;
using UnityEngine;
using Zenject;

namespace ODckSoundManager.SoundManagerZenject
{
    //Inject this class so it can be used everywhere
    public class SoundManager : IInitializable, IDisposable
    {
        private AudioSource _musicSource; //Add binding in scene
        private AudioSource _sfxSource; //Add binding in scene
        [Inject] private ManagerAudioClips _clips;
        [Inject] private SoundSettings _settings;

        private readonly CompositeDisposable _disposable = new CompositeDisposable();

        public void Construct(AudioSource musicSource, AudioSource sfxSource)
        {
            _musicSource = musicSource;
            _sfxSource = sfxSource;
            EditAudioSourceVolume();
        }
        
        public void Initialize()
        {
            _settings.onConfigurationChanged.Subscribe(x => EditAudioSourceVolume()).AddTo(_disposable);
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }

        private void EditAudioSourceVolume()
        {
            _musicSource.volume = _settings.MusicVolume * _settings.MasterVolume;
            _musicSource.mute = _settings.MuteAll;
            _sfxSource.volume = _settings.SfxVolume * _settings.MasterVolume;
            _sfxSource.mute = _settings.MuteAll;
        }

        private void PlaySfx(AudioClip clip, float volume = 1)
        {
            _sfxSource.PlayOneShot(clip, volume);
        }

        /// <summary>
        /// Play sfx by name
        /// </summary>
        /// <param name="clipName"></param>
        /// <param name="volume"></param>
        public void PlaySfx(string clipName, float volume = 1)
        {
            if (!_clips.clips.TryGetValue(clipName, out var clip))
            {
                Debug.LogError($"clip {clipName} not found");
                return;
            }

            PlaySfx(clip, volume);
        }

        private void PlayMusic(AudioClip clip)
        {
            _musicSource.clip = clip;
            _musicSource.Play();
        }

        /// <summary>
        /// Play clip by name
        /// </summary>
        /// <param name="clipName"></param>
        public void PlayMusic(string clipName)
        {
            if (!_clips.clips.TryGetValue(clipName, out var clip))
            {
                Debug.LogError($"clip {clipName} not found");
                return;
            }

            ;
            PlayMusic(clip);
        }
    }
}