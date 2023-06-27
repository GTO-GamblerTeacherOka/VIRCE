using Lobby;
using Pixiv.VroidSdk;
using UnityEngine;
using Zenject;

namespace VRoid
{
    public class ModelManager : MonoBehaviour
    {
        [SerializeField] private Camera camera;
        [Inject] private DiContainer _container;
        
        private void Start()
        {
            Auth.Api.GetAccountCharacterModels(10, models =>
            {
                ModelLoader.LoadVrm(models[0], vrm =>
                {
                    vrm.transform.localPosition = new Vector3(0, 0, 0);
                    vrm.transform.localRotation = Quaternion.Euler(0, 180, 0);
                    vrm.transform.localScale = Vector3.one * 3;

                    _container.InstantiateComponent<PlayerControl>(vrm);

                    Transform transform1;
                    (transform1 = camera.transform).SetParent(vrm.transform);
                    transform1.localPosition = new Vector3(0, 1, -1.5f);
                    
                    var colliderComponent = vrm.gameObject.AddComponent<CapsuleCollider>();
                    var height = vrm.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Head).position.y - vrm.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Hips).position.y;
                    colliderComponent.height = height;
                    colliderComponent.radius = height / 8;
                    colliderComponent.center = new Vector3(0, height / 2, 0);
                    var rigitBodyComponent = vrm.gameObject.AddComponent<Rigidbody>();
                    rigitBodyComponent.useGravity = true;
                    rigitBodyComponent.mass = 60;
                    rigitBodyComponent.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
                }, progress =>
                {
                    Debug.Log(progress);
                }, _ => {});
            }, _ =>{});
        }
    }
}
