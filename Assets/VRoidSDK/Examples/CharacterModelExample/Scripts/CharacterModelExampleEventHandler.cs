using VRoidSDK;
using VRoidSDK.Examples.Core.Localize;
using UnityEngine;

namespace VRoidSDK.Examples.CharacterModelExample
{
    public abstract class CharacterModelExampleEventHandler : MonoBehaviour
    {
        public abstract void OnModelLoaded(string modelId, GameObject go);
        public abstract void OnLangChanged(Translator.Locales locale);
    }
}
