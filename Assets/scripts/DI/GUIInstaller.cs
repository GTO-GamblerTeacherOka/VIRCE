using UnityEngine;
using Zenject;

namespace DI
{
    public class GUIInstaller : Installer<GUISkin, Texture2D, GUIInstaller>
    {
        private readonly GUISkin _skin;
        private readonly Texture2D _closeButtonTexture;
        
        public GUIInstaller(GUISkin skin, Texture2D closeButtonTexture)
        {
            _skin = skin;
            _closeButtonTexture = closeButtonTexture;
        }
        
        public override void InstallBindings()
        {
            Container.Bind<GUISkin>().FromInstance(_skin).AsCached();
            Container.Bind<Texture2D>().WithId("closeButtonTexture").FromInstance(_closeButtonTexture).AsCached();
        }
    }
}