using Lobby.Chat.DataBase;
using Zenject;

namespace DI
{
    public class ChatDataBaseInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IChatDataBase>().To<LocalDatabase>().AsCached();
        }
    }
}