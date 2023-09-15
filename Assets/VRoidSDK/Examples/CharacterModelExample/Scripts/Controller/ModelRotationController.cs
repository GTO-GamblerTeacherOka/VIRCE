using UnityEngine;

namespace VRoidSDK.Examples.CharacterModelExample.Controller
{
    public class ModelRotationController : MonoBehaviour
    {
        private Vector3 _lastMousePosition;

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _lastMousePosition = Input.mousePosition;
            }

            if (Input.GetMouseButton(0))
            {
                transform.rotation *= Quaternion.Euler(0, (_lastMousePosition - Input.mousePosition).x / Screen.width * 180, 0);
                _lastMousePosition = Input.mousePosition;
            }
        }

        public void Reset()
        {
            transform.rotation = Quaternion.identity;
        }
    }
}
