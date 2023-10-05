using UnityEngine;
using UnityEngine.UI;

namespace VRoidSDK.Examples.Core.View.Resize
{
    public abstract class LoadImageFitter : MonoBehaviour
    {
        public abstract void Fit(RawImage image, Texture2D texture);
    }
}
