using UnityEngine;

namespace VRoidSDK.Examples.Core.View.Parts
{
    public class Button : BaseView
    {
#pragma warning disable 0649
        [SerializeField] private UnityEngine.UI.Button _button;
        public Message Message;
#pragma warning restore 0649

        public bool Enable
        {
            get
            {
                return _button.interactable;
            }
            set
            {
                _button.interactable = value;
            }
        }
    }
}
