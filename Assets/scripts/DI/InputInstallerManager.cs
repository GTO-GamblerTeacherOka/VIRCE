using IO;
using Zenject;

namespace DI
{
    public class InputInstallerManager : MonoInstaller
    {
        public override void InstallBindings()
        {
            InputInstaller.Install(Container);
        }
    }
}
