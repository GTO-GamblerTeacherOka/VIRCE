using UnityEngine;
using Zenject;

namespace DI
{
    public class GUIInstaller : Installer<GUISkin, GUIInstaller>
    {
        private GUISkin _skin;
        
        public GUIInstaller(GUISkin skin)
        {
            _skin = skin;
        }
        
        public override void InstallBindings()
        {
            Container.Bind<GUISkin>().FromInstance(_skin).AsCached();
        }
    }
}