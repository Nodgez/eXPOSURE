using Exposure.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.iOS;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var _actor = GetComponent<A_InputActor>();
        var _handler = GetComponent<IInputHandler>();
        _handler.Initialize(_actor);
    }
}
