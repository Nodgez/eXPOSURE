using UnityEngine;
using UnityEngine.InputSystem;

namespace Exposure.Input
{
    public abstract class A_InputActor : MonoBehaviour
    {
        [SerializeField] protected Transform targetObject;
        [SerializeField] protected float speed = 5f;
        [SerializeField] protected float dashForce = 100f;
        [SerializeField] protected LayerMask movementBlockingMask;

        protected RaycastHit movementBlockerInfo;
        protected Vector3 heading = Vector3.zero;

        public abstract void Movement_performed(InputAction.CallbackContext obj);
        public abstract void Movement_canceled(InputAction.CallbackContext obj);
        public abstract void Dash_performed(InputAction.CallbackContext obj);
        public abstract void Dash_canceled(InputAction.CallbackContext obj);

        private void OnDrawGizmos()
        {
            if (targetObject == null) return;

            Gizmos.color = Color.red;
            Gizmos.DrawRay(targetObject.position, heading);
            Gizmos.DrawRay(movementBlockerInfo.point, movementBlockerInfo.normal);
        }
    }
}