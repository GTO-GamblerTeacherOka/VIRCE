namespace Lobby.Chat.DataBase
{
    public interface IChatDataBase
    {
        public void AddMessage(in Message msg);
        public Message[] GetMessages();
    }
}
