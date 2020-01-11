using System.Globalization;
using UnityEditor;
using UnityEngine;

namespace ODckSoundManager.Settings.Editor
{
    public class SoundSettingsWindow : EditorWindow
    {

        [MenuItem("Tools/ODckSoundManager")]
        private static void ShowWindow()
        {
            
            var window = GetWindow<SoundSettingsWindow>();
            window.titleContent = new GUIContent("ODck Sound Manager");
            window.Show();
        }

        private void OnGUI()
        {
            var settings = new SoundSettings();
            settings.MasterVolume = EditorGUILayout.Slider("Master Volume", settings.MasterVolume, 0, 1);
            settings.MusicVolume =
                EditorGUILayout.Slider("Music Volume", settings.MusicVolume, 0, 1);
            GUILayout.Label($"Final Music Volume: {settings.MusicVolume * settings.MasterVolume}");
            settings.SfxVolume = EditorGUILayout.Slider("Sfx Volume", settings.SfxVolume, 0, 1);
            GUILayout.Label($"Final Sfx Volume: {settings.SfxVolume * settings.MasterVolume}");
            settings.MuteAll = EditorGUILayout.Toggle("Mute all", settings.MuteAll);
        }
    }
}