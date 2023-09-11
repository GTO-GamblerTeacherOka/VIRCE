using System;
using UnityEngine;

namespace VRoidSDK.Examples.Core.View.Cache
{
    public class LoadImageCache
    {
        public DateTime LastAccessTime { get; set; }
        public Texture2D Texture { get; set; }
    }
}
