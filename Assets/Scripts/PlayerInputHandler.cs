using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
namespace Exposure.Input
{
    public class PlayerInputHandler : MonoBehaviour, IInputHandler
    {
        private PlayerInputScheme inputActions;        
        public void Initialize(A_InputActor inputActor)
        {
            inputActions = new PlayerInputScheme();
            inputActions.Enable();

            var gamePadConnected = Gamepad.all.Count > 0;
            if (gamePadConnected)
            {
                inputActions.Gamepad.Movement.performed += inputActor.Movement_performed;
                inputActions.Gamepad.Movement.canceled += inputActor.Movement_canceled;
                inputActions.Gamepad.Dash.performed += inputActor.Dash_performed;
                inputActions.Gamepad.Dash.canceled += inputActor.Dash_canceled;
            }

            else
            {
                inputActions.Keyboard.Movement.performed += inputActor.Movement_performed;
                inputActions.Keyboard.Movement.canceled += inputActor.Movement_canceled;
                inputActions.Keyboard.Dash.performed += inputActor.Dash_performed;
                inputActions.Keyboard.Dash.canceled += inputActor.Dash_canceled;
            }
        }


    }
}