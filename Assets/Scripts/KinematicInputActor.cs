using Exposure.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static Cinemachine.CinemachineOrbitalTransposer;

namespace Exposure.Input
{
    public class KinematicInputActor : A_InputActor
    {
        public override void Dash_canceled(InputAction.CallbackContext obj)
        {
            print("Dash Complete");
            transform.position += heading * speed * Time.deltaTime * dashForce;
        }

        public override void Dash_performed(InputAction.CallbackContext obj)
        {
        }

        public override void Movement_canceled(InputAction.CallbackContext obj)
        {
            heading = Vector3.zero;
        }

        public override void Movement_performed(InputAction.CallbackContext obj)
        {
            var iVal = obj.ReadValue<Vector2>();
            heading = new Vector3(iVal.x, 0f, iVal.y);
        }

        private void Update()
        {
            var isPointingAtMovementBlocker = Physics.Raycast(targetObject.position, heading.normalized, out movementBlockerInfo, 1f, movementBlockingMask);

            if (isPointingAtMovementBlocker)
            {
                if (heading.normalized * -1 == movementBlockerInfo.normal)
                    heading = Vector3.zero;
                else
                {
                    var dot = Vector3.Dot((targetObject.position - movementBlockerInfo.point).normalized, movementBlockerInfo.normal);
                    var projection = dot / movementBlockerInfo.normal.sqrMagnitude;
                    heading = projection * movementBlockerInfo.normal;
                    heading.Normalize();
                }
            }
            targetObject.position += heading * speed * Time.deltaTime;
        }
    }
}