namespace Lobby.Chat.DataBase
{
    /// <summary>
    /// Interface for chat data base
    /// </summary>
    public interface IChatDataBase
    {
        public void AddMessage(in Message msg);
        public Message[] GetMessages();
    }
}
