using UnityEngine;
using UnityEngine.UI;
using Pixiv.VroidSdk.Api.DataModel; // Add DataModel reference

namespace VRoidSDK.Examples.Core.View.Parts
{
    public class IconText : BaseView
    {
#pragma warning disable 0649
        [SerializeField] private Text _text;
        [SerializeField] private LoadImage _icon;
#pragma warning restore 0649

        public void Set(string text, WebImage icon)
        {
            _text.text = text;
            _icon.Load(icon);
        }
    }
}
