using System.Collections.Generic;
using System.Linq;
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
        public static readonly Dictionary<byte, GameObject> Models = new();
        public static readonly Queue<(byte userId, string modelId)> LoadModelQueue = new();
        public static readonly List<byte> DeleteUserIds = new();
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
                    var height = vrm.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Head).position.y;
                    colliderComponent.height = height;
                    colliderComponent.radius = height / 8;
                    colliderComponent.center = new Vector3(0, height / 2, 0);
                    var rigitBodyComponent = vrm.gameObject.AddComponent<Rigidbody>();
                    rigitBodyComponent.useGravity = true;
                    rigitBodyComponent.mass = 60;
                    rigitBodyComponent.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
                    rigitBodyComponent.constraints = RigidbodyConstraints.FreezeRotation;

                    Models[GameSetting.UserId] = vrm;
                }, progress => { Debug.Log(progress); }, _ => { });
            }, _ => { });
        }

        private void FixedUpdate()
        {
            while (LoadModelQueue.Count > 0)
            {
                var (userId, modelId) = LoadModelQueue.Dequeue();
                LoadOtherPlayerModel(userId, modelId);
            }

            LoadModelQueue.Clear();

            foreach (var userId in DeleteUserIds.Where(userId => Models.Keys.Contains(userId)))
            {
                Destroy(Models[userId]);
                Models.Remove(userId);
            }

            DeleteUserIds.Clear();
        }

        public void LoadOtherPlayerModel(byte userId, string modelId)
        {
            MultiplayModelLoader.LoadVrm(modelId, vrm =>
            {
                Models[userId] = vrm;

                vrm.gameObject.layer = 11;

                vrm.transform.localPosition = new Vector3(0, 0, 0);
                vrm.transform.localRotation = Quaternion.Euler(0, 0, 0);
                vrm.transform.localScale = Vector3.one;

                var animator = vrm.GetComponent<Animator>();
                animator.runtimeAnimatorController = _animatorController;

                var colliderComponent = vrm.gameObject.AddComponent<CapsuleCollider>();
                var height = animator.GetBoneTransform(HumanBodyBones.Head).position.y -
                             animator.GetBoneTransform(HumanBodyBones.Hips).position.y;
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
            }, _ => { }, _ => { });
        }
    }
}