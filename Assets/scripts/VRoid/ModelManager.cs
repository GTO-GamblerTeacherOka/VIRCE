using Pixiv.VroidSdk;
using UnityEngine;

namespace VRoid
{
    public class ModelManager : MonoBehaviour
    {
        private void Start()
        {
            Auth.Api.GetAccountCharacterModels(10, models =>
            {
                ModelLoader.LoadVrm(models[0], vrm =>
                {
                    vrm.transform.localPosition = new Vector3(0, 0, 0);
                    vrm.transform.localRotation = Quaternion.Euler(0, 180, 0);
                    vrm.transform.localScale = Vector3.one * 5;
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
