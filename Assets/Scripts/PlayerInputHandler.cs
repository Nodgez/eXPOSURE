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

            inputActions.PlayerNavigation.Movement.performed += inputActor.Movement_performed;
            inputActions.PlayerNavigation.Movement.canceled += inputActor.Movement_canceled;
            inputActions.PlayerNavigation.Dash.performed += inputActor.Dash_performed;
            inputActions.PlayerNavigation.Dash.canceled += inputActor.Dash_canceled;
            inputActions.PlayerNavigation.DropPickup.performed += inputActor.DropPickup_performed;
        }
    }
}