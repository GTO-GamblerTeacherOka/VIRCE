using IO;
using UnityEngine;
using Util;
using Zenject;

// ReSharper disable Unity.InefficientPropertyAccess

namespace Lobby
{
    /// <summary>
    ///     Class for controlling player
    /// </summary>
    public class PlayerControl : MonoBehaviour
    {
        private static readonly int Speed = Animator.StringToHash("speed");

        [SerializeField] private float moveSpeed = 1.0f;

        [SerializeField] private float viewPointSpeed = 10.0f;

        public Animator animator;
        private bool _isColliding;

        private Vector3 _move;

        [Inject] private IMoveProvider _moveProvider;

        private Rigidbody _rigidbody;

        [Inject] private IViewPointProvider _viewPointProvider;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            var moveComplex = _moveProvider.GetMove();
            var rotationComplex = new Complex(transform.eulerAngles, true);
            var move = (moveComplex * rotationComplex.Conjugate).Normalize.ToVector3 * moveSpeed;

            if (move.sqrMagnitude is not 0)
                animator.SetFloat(Speed, 1.0f);
            else
                animator.SetFloat(Speed, 0);
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.AddForce(move, ForceMode.VelocityChange);

            var horizontalViewPoint = _viewPointProvider.GetHorizontalViewPoint();
            transform.RotateAround(transform.position, Vector3.up,
                horizontalViewPoint * (viewPointSpeed * Time.deltaTime));
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.transform.gameObject.layer != 6)
                _isColliding = true;
        }

        private void OnCollisionExit(Collision other)
        {
            if (other.transform.gameObject.layer != 6)
                _isColliding = false;
        }
    }
}