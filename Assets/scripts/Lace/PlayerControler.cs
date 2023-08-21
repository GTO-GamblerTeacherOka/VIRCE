using System.Collections;
using System.Collections.Generic;
using IO;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

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
        
    }
}
