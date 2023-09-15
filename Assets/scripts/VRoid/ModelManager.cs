using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Lobby;
using Pixiv.VroidSdk;
using Settings;
using UnityEngine;
using Zenject;

// ReSharper disable BitwiseOperatorOnEnumWithoutFlags
// ReSharper disable Unity.InefficientPropertyAccess

namespace VRoid
{
    /// <summary>
    ///     Class for managing VRoid models
    /// </summary>
    public class ModelManager : MonoBehaviour
    {
        private static readonly Dictionary<byte, GameObject> Models = new();
        private static readonly Dictionary<byte, string> ModelIds = new();
        [SerializeField] private Camera vcam;
        [Inject] private RuntimeAnimatorController _animatorController;
        [Inject] private DiContainer _container;

        private void Start()
        {
            Auth.Api.GetCharacterModels(GameSetting.ModelId, model =>
            {
                ModelLoader.LoadVrm(model.character_model, vrm =>
                {
                    vrm.gameObject.layer = 11;

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
                    var height = vrm.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Head).position.y -
                                 vrm.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Hips).position.y;
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
                    rigitBodyComponent.collisionDetectionMode = CollisionDetectionMode.Continuous;
                }, progress => { Debug.Log(progress); }, _ => { });
            }, _ => { });
            UniTask.RunOnThreadPool(() =>
            {
                var license = GameSetting.ModelPublishId;
                // TODO: send model license to server
            }).Forget();
        }

        private void Update()
        {
            var noLoadedKeys = ModelIds.Keys.ToArray().Where(k => !Models.Keys.Contains(k));
            foreach (var key in noLoadedKeys) LoadOtherPlayerModel(key, ModelIds[key]);
        }

        public void LoadOtherPlayerModel(byte userId, string modelId)
        {
            MultiplayModelLoader.LoadVrm(modelId, vrm =>
            {
                vrm.gameObject.layer = 11;

                vrm.transform.localPosition = new Vector3(0, 0, 0);
                vrm.transform.localRotation = Quaternion.Euler(0, 0, 0);
                vrm.transform.localScale = Vector3.one;

                var animator = vrm.GetComponent<Animator>();
                animator.runtimeAnimatorController = _animatorController;

                vcam.transform.SetParent(vrm.transform);
                vcam.transform.localPosition = new Vector3(0, 1f, -2);
                vcam.transform.localEulerAngles = new Vector3(0, 0, 0);

                var colliderComponent = vrm.gameObject.AddComponent<CapsuleCollider>();
                var height = vrm.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Head).position.y -
                             vrm.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Hips).position.y;
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
                rigitBodyComponent.collisionDetectionMode = CollisionDetectionMode.Continuous;

                Models[userId] = vrm;
            }, _ => { }, _ => { });
        }

        public static void AddModel(byte userId, string modelId)
        {
            ModelIds[userId] = modelId;
        }
    }
}