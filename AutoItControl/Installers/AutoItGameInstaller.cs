using Zenject;
using AutoItControl.Models;

namespace AutoItControl.Installers
{
    public class AutoItGameInstaller : Installer
    {
        public override void InstallBindings()
        {
            this.Container.BindInterfacesAndSelfTo<AutoItController>().AsCached().NonLazy();
        }
    }
}
