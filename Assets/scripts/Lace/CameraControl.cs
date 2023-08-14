using UnityEngine;

namespace Lace
{
    public class CameraControl : MonoBehaviour
    {
        [SerializeField] private Camera vcam;

        private void Start()
        {
            vcam.transform.SetParent();
            vcam.transform.localPosition = new Vector3(0, 1f, -2);
            vcam.transform.localEulerAngles = new Vector3(0, 0, 0);
        }
    }
}

