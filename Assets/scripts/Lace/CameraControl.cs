using UnityEngine;

namespace Lace
{
    public class CameraControl : MonoBehaviour
    {
        [SerializeField] private Camera camera;

        private void Start()
        {
            camera.transform.SetParent(transform);
            camera.transform.localPosition = new Vector3(0, 1f, -2);
            camera.transform.localEulerAngles = new Vector3(0, 0, 0);
        }
    }
}

