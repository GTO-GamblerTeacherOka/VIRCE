using UnityEngine;
using UnityEngine.UI;

namespace VRoidSDK.Examples.Core.View.Parts
{
    public class Message : BaseView
    {
#pragma warning disable 0649
        [SerializeField] private Text _text;
#pragma warning restore 0649

        public string Text
        {
            get
            {
                return _text.text;
            }
            set
            {
                _text.text = value;
            }
        }
    }
}
