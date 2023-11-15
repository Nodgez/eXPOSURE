using Exposure.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.CinemachineOrbitalTransposer;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PhysicsInputActor : A_InputActor
{
    private new Rigidbody  rigidbody;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();

        rigidbody.maxLinearVelocity = 10f;
    }
    public override void Dash_canceled(InputAction.CallbackContext obj)
    {
        print("Dash Complete");
        rigidbody.AddForce(heading * dashForce, ForceMode.Impulse);
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
            return;
        rigidbody.AddForce(heading * speed, ForceMode.Force);

    }
}