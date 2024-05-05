using Zenject;
using UnityEngine;
using Scripts.Control;

namespace Scripts.Zenject
{
    public class SceneInstaller : MonoInstaller
    {
        public DesktopInput DesktopInput;
        public MobileInput MobileInput;

        public override void InstallBindings()
        {
            if (SystemInfo.deviceType == DeviceType.Desktop)
                Container.Bind<IInput>().To<DesktopInput>().FromInstance(DesktopInput);
            else if (SystemInfo.deviceType == DeviceType.Handheld)
                Container.Bind<IInput>().To<MobileInput>().FromInstance(MobileInput);

            Container.Bind<MovementHandler>().AsSingle().NonLazy();
        }
    }
}
