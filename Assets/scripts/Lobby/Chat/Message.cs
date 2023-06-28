using System;
using UnityEngine;

namespace Lobby.Chat
{
    public class Message
    {
        public readonly string Id;
        public readonly string UserName;
        public readonly string Text;
        public readonly DateTime Time;
        
        public Message(string id, string userName, string text, DateTime time)
        {
            Id = id;
            UserName = userName;
            Text = text;
            Time = time;
        }
        
        public Message FromJson(string json)
        {
            var message = JsonUtility.FromJson<Message>(json);
            return message;
        }
        
        public string ToJson()
        {
            var json = JsonUtility.ToJson(this);
            return json;
        }
        
        public override string ToString()
        {
            return $"{Time} {Id}, {UserName}: {Text}";
        }
    }
}
