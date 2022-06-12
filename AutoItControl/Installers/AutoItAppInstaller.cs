using Zenject;
using AutoItControl.Models;

namespace AutoItControl.Installers
{
    internal class AutoItAppInstaller : Installer
    {
        public override void InstallBindings()
        {
            this.Container.BindInterfacesAndSelfTo<AutoItData>().AsSingle();
        }
    }
}
