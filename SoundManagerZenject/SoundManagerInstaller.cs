using ODckSoundManager.Settings;
using Zenject;

namespace ODckSoundManager.SoundManagerZenject
{
    public class SoundManagerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<SoundManager>().AsSingle();
            Container.Bind<SoundSettings>().AsSingle();
        }
    }
}