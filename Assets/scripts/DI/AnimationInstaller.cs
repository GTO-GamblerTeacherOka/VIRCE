using UnityEngine;
using Zenject;

namespace DI
{
    public class AnimationInstaller : MonoInstaller
    {
        [SerializeField]
        private RuntimeAnimatorController animatorController;
        public override void InstallBindings()
        {
            Container.Bind<RuntimeAnimatorController>().FromInstance(animatorController).AsCached();
        }
    }
}