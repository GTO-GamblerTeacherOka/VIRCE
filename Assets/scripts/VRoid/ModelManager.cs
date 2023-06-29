using Lobby;
using Pixiv.VroidSdk;
using UnityEditor.Animations;
using UnityEngine;
using Zenject;
// ReSharper disable BitwiseOperatorOnEnumWithoutFlags

namespace VRoid
{
    /// <summary>
    /// Class for managing VRoid models
    /// </summary>
    public class ModelManager : MonoBehaviour
    {
        [SerializeField] private Camera vcam;
        [Inject] private DiContainer _container;
        [Inject] private AnimatorController _animatorController;
        
        private void Start()
        {
            Auth.Api.GetAccountCharacterModels(10, models =>
            {
                ModelLoader.LoadVrm(models[0], vrm =>
                {
                    vrm.transform.localPosition = new Vector3(0, 0, 0);
                    vrm.transform.localRotation = Quaternion.Euler(0, 0, 0);
                    vrm.transform.localScale = Vector3.one;
                    
                    var animator = vrm.GetComponent<Animator>();
                    animator.runtimeAnimatorController = _animatorController;
                    
                    vcam.transform.SetParent(vrm.transform);
                    vcam.transform.localPosition = new Vector3(0, 1f, -2);
                    vcam.transform.localEulerAngles = new Vector3(0, 0, 0);

                    var control = _container.InstantiateComponent<PlayerControl>(vrm);
                    control.animator = animator;
                    
                    var colliderComponent = vrm.gameObject.AddComponent<CapsuleCollider>();
                    var height = vrm.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Head).position.y - vrm.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Hips).position.y;
                    colliderComponent.height = height;
                    colliderComponent.radius = height / 8;
                    colliderComponent.center = new Vector3(0, height / 2, 0);
                    var rigitBodyComponent = vrm.gameObject.AddComponent<Rigidbody>();
                    rigitBodyComponent.useGravity = true;
                    rigitBodyComponent.mass = 60;
                    rigitBodyComponent.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
                    rigitBodyComponent.constraints = RigidbodyConstraints.FreezeRotation
                                                     | RigidbodyConstraints.FreezePositionX
                                                     | RigidbodyConstraints.FreezePositionZ;
                }, progress =>
                {
                    Debug.Log(progress);
                }, _ => {});
            }, _ =>{});
        }
    }
}
