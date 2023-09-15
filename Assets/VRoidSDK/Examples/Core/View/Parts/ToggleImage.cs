using VRoidSDK.Examples.Core.View;
using UnityEngine;
using UnityEngine.UI;

namespace VRoidSDK.Examples.Core.View.Parts
{
    public class ToggleImage : BaseView
    {
#pragma warning disable 0649
        [SerializeField] private Image _activeImage;
        [SerializeField] private Image _nonActiveImage;
#pragma warning restore 0649

        public bool ToggleActive
        {
            set
            {
                _activeImage.gameObject.SetActive(value);
                _nonActiveImage.gameObject.SetActive(!value);
            }
        }
    }
}
