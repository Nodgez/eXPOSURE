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
#if UNITY_EDITOR
        public bool IsTestingCameraConvertedInput;
#endif
        [SerializeField] Camera _camera;

        private Vector3 collisionOffset;
        private bool isDashLocked;

        public override void Dash_canceled(InputAction.CallbackContext obj)
        {
        }

        public override void Dash_performed(InputAction.CallbackContext obj)
        {
            print("Dash Performed");
            if (isDashLocked)
                return;

            Timer.CreateTimer(1.5f,
                            () =>
                            {
                                isDashLocked = false;
                                print("Dash Timer Complete");
                            },
                            "Dash Timer");
            transform.position += heading * speed * Time.deltaTime * dashDistance;
            isDashLocked = true;
        }

        public override void DropPickup_performed(InputAction.CallbackContext obj)
        {

        }

        public override void Movement_canceled(InputAction.CallbackContext obj)
        {
            heading = Vector3.zero;
        }

        public override void Movement_performed(InputAction.CallbackContext obj)
        {
            var iVal = obj.ReadValue<Vector2>();
            var cameraDirectionality = _camera.transform.TransformDirection(new Vector3(iVal.x, iVal.y));
            if (IsTestingCameraConvertedInput)
                heading = new Vector3(cameraDirectionality.x, 0f, cameraDirectionality.z).normalized;
            else
                heading = new Vector3(iVal.x, 0, iVal.y);
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
                    collisionOffset = projection * movementBlockerInfo.normal;
                }
            }
            else
                collisionOffset = Vector3.zero;

            targetObject.position += (heading + collisionOffset) * speed * Time.deltaTime;
        }
    }
}