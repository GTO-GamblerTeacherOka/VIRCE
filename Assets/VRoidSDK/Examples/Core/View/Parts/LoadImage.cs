using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Pixiv.VroidSdk.Api.DataModel;
using VRoidSDK.Examples.Core.View.Cache;

namespace VRoidSDK.Examples.Core.View.Parts
{
    public class LoadImage : BaseView
    {
#pragma warning disable 0649
        [SerializeField] private RawImage _rawImage;
        [SerializeField] private Resize.LoadImageFitter _fitter;
#pragma warning restore 0649

        public void Load(WebImage image)
        {
            StartCoroutine(LoadWebImage(image));
        }

        public void Load(Texture2D texture)
        {
            _rawImage.color = Color.white;
            _rawImage.texture = texture;
            if (_fitter != null)
            {
                _fitter.Fit(_rawImage, texture);
            }
        }

        private IEnumerator LoadWebImage(WebImage image)
        {
            var textureCache = LoadImageCacheStorage.Instance.Load(image.url);
            if (textureCache != null)
            {
                Load(textureCache);
                yield break;
            }

            using (var request = UnityWebRequestTexture.GetTexture(image.url))
            {
                yield return request.SendWebRequest();
                if (request.result != UnityWebRequest.Result.Success) yield break;
                var texture = DownloadHandlerTexture.GetContent(request);
                Load(texture);
                LoadImageCacheStorage.Instance.Save(image.url, texture);
            }
        }
    }
}
