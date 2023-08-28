using Cysharp.Threading.Tasks;
using Lobby;
using Pixiv.VroidSdk;
using Pixiv.VroidSdk.Api;
using UnityEngine;
using Zenject;
// ReSharper disable BitwiseOperatorOnEnumWithoutFlags
// ReSharper disable Unity.InefficientPropertyAccess

namespace VRoid
{
    /// <summary>
    /// Class for managing VRoid models
    /// </summary>
    public class ModelManager : MonoBehaviour
    {
        [SerializeField] private Camera vcam;
        [Inject] private DiContainer _container;
        [Inject] private RuntimeAnimatorController _animatorController;
        
        private void Start()
        {
            Auth.Api.GetAccountCharacterModels(10, models =>
            {
                var model = models[0];
                var modelId = model.id;
                var multiplayApi = new MultiplayApi(Auth.OauthClient);
                UniTask.RunOnThreadPool(async () =>
                {
                    var license = await multiplayApi.PostDownloadLicensesAsync(modelId);
                    // TODO: send model license to server
                }).Forget();
                ModelLoader.LoadVrm(model, vrm =>
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
                    rigitBodyComponent.collisionDetectionMode = CollisionDetectionMode.Continuous;
                }, progress =>
                {
                    Debug.Log(progress);
                }, _ => {});
            }, _ =>{});
        }

        public void LoadOtherPlayerModel(string modelId)
        {
            MultiplayModelLoader.LoadVrm(modelId, vrm =>
            {
                //TODO: set user info
            },_=>{}, _ => {});
        }
    }
}
