using UnityEditor.Animations;
using UnityEngine;
using Zenject;

namespace DI
{
    public class AnimationInstaller : MonoInstaller
    {
        [SerializeField]
        private AnimatorController animatorController;
        public override void InstallBindings()
        {
            Container.Bind<AnimatorController>().FromInstance(animatorController).AsCached();
        }
    }
}