using UnityEngine;
using UnityEngine.UI;

namespace VRoidSDK.Examples.Core.View.Parts
{
    public class ProgressBar : BaseView
    {
#pragma warning disable 0649
        [SerializeField] private Slider _slider;
#pragma warning restore 0649
        public float Value
        {
            get
            {
                return _slider.value;
            }
            set
            {
                _slider.value = value;
            }
        }
    }
}
