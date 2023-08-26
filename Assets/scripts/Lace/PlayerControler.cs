using System;
using IO;
using UnityEngine;
using Util;
using Zenject;
using Vector3 = UnityEngine.Vector3;

public class PlayerControler : MonoBehaviour
{
    [Inject] 
    private IMoveProvider _moveProvider;
    [Inject]
    private IViewPointProvider _viewPointProvider;

    [SerializeField] 
    private float moveSpeed = 1.0f;

    private Vector3 _move;
    
    private void Update()
    {
        var moveComplex = _moveProvider.GetMove();
        var rotationComplex = new Complex(transform.eulerAngles, true);

        transform.position += rotationComplex.Conjugate.Normalize.ToVector3 * moveSpeed;
        
        //todo: rotate player the Y axis
        var horizontalRotation = (moveComplex * rotationComplex.Conjugate).Normalize.ToVector3;
        transform.RotateAround(transform.position, Vector3.up, (horizontalRotation * (moveSpeed * Time.deltaTime)).magnitude);
        //todo: rotate camera the X axis
    }
}
