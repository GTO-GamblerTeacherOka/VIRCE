using IO;
using Zenject;

namespace DI
{
    public class InputInstaller : Installer<InputInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<IMoveProvider>().To<MoveFromKey>().AsTransient();
            Container.Bind<IViewPointProvider>().To<ViewPointFromMouse>().AsTransient();
        }
    }
}