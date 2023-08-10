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
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
