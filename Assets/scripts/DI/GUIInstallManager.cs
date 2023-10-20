using UnityEngine;
using Zenject;

namespace DI
{
    public class GUIInstallManager : MonoInstaller
    {
        [SerializeField]
        private GUISkin skin;
        [SerializeField]
        private Texture2D closeButtonTexture;
        
        public override void InstallBindings()
        {
            GUIInstaller.Install(Container, skin, closeButtonTexture);
        }
    }
}