using UnityEngine;
using Zenject;
using IO;

namespace Lace
{
    public class CameraControl : MonoBehaviour
    {
        [Inject]
        private IViewPointProvider _viewPointProvider;
    }
}

