using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRoidSDK.Examples.Core.View.Parts;

namespace Lobby
{
    /// <summary>
    /// Class for display name
    /// </summary>
    public class DisplayName : MonoBehaviour
    {
        public readonly string UserId;
        public readonly string NewDisplayName;
        
        public DisplayName(string userId, string newDisplayName)
        {
            UserId = userId;
            NewDisplayName = newDisplayName;
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
            return $"{UserId}, {NewDisplayName}";
        }
    }
}
