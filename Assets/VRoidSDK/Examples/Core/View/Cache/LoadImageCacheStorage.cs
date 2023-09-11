using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRoidSDK.Examples.Core.View.Cache
{
    public class LoadImageCacheStorage
    {
        private const int MaxCashedSize = 100;
        private const int ReduceCacheCount = 10;

        private static LoadImageCacheStorage s_instance;
        public static LoadImageCacheStorage Instance
        {
            get
            {
                if (s_instance == null)
                {
                    s_instance = new LoadImageCacheStorage();
                }

                return s_instance;
            }
        }

        private Dictionary<string, LoadImageCache> _cache = new Dictionary<string, LoadImageCache>();

        public void Save(string url, Texture2D texture)
        {
            if (_cache.Count() >= MaxCashedSize)
            {
                CleanCache();
            }

            if (_cache.ContainsKey(url))
            {
                var data = _cache[url];
                data.LastAccessTime = DateTime.Now;
                data.Texture = texture;
                return;
            }

            _cache[url] = new LoadImageCache()
            {
                LastAccessTime = DateTime.Now,
                Texture = texture
            };
        }

        public Texture2D Load(string url)
        {
            if (_cache.ContainsKey(url))
            {
                var data = _cache[url];
                data.LastAccessTime = DateTime.Now;
                return data.Texture;
            }

            return null;
        }

        private void CleanCache()
        {
            _cache = _cache.OrderByDescending((kvp) => kvp.Value.LastAccessTime)
                            .Take(MaxCashedSize - ReduceCacheCount)
                            .ToDictionary((kvp) => kvp.Key, (kvp) => kvp.Value);
        }
    }
}
