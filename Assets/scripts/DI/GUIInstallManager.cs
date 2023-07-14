using UnityEngine;
using Zenject;

namespace DI
{
    public class GUIInstallManager : MonoInstaller
    {
        [SerializeField]
        private GUISkin skin;
        
        public override void InstallBindings()
        {
            GUIInstaller.Install(Container, skin);
        }
    }
}