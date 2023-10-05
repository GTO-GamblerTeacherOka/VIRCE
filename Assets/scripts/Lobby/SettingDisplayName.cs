using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Protocol;

namespace Lobby
{ 
    /// <summary>
    /// Class for setting display name
    /// </summary>
    public class SettingDisplayName : MonoBehaviour
    {
        public void SendDisplayName(in DisplayName displayName)
        {
            var newDisplayName = string.Empty;
            Api.SendDisplayName(newDisplayName);
        }
    }
}
