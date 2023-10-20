using System.Collections.Generic;

namespace Lobby.Chat.DataBase
{
    /// <summary>
    /// Class for local chat data base
    /// </summary>
    public class LocalDatabase : IChatDataBase
    {
        private static readonly List<Message> DataBase = new();

        public void AddMessage(in Message msg)
        {
            DataBase.Add(msg);
        }
        
        public Message[] GetMessages()
        {
            return DataBase.ToArray();
        }
    }
}
