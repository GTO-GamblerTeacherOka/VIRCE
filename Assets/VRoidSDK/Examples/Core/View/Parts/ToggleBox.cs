using VRoidSDK.Examples.Core.View;
using UnityEngine;
using UnityEngine.UI;

namespace VRoidSDK.Examples.Core.View.Parts
{
    public class ToggleBox : BaseView
    {
#pragma warning disable 0649
        [SerializeField] private UnityEngine.UI.Toggle _toggle;
#pragma warning restore 0649
        public Message Message;

        public bool Checked
        {
            get
            {
                return _toggle.isOn;
            }
            set
            {
                _toggle.isOn = value;
            }
        }
    }
}
