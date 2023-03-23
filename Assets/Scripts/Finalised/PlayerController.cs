using UnityEngine;
using Unity.Netcode;
using UnityEditor;

namespace Finalised
{
    /// <summary>
    /// Currently Client-Authoratative movement.
    /// </summary>
    public class PlayerController : NetworkBehaviour
    {
        [SerializeField] private float speed = 3f;
        private Camera _mainCamera;
        private Vector3 _mouseInput = Vector3.zero;

        private void Initialise()
        {
            _mainCamera = Camera.main;
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            Initialise();
        }

        private void Update()
        {
            // A guard clause that prevents movement and rotation from being executed if we're using another window.
            if (!IsOwner || !Application.isFocused) return;

            // Movement
            _mouseInput.x = Input.mousePosition.x;
            _mouseInput.y = Input.mousePosition.y;
            _mouseInput.z = _mainCamera.nearClipPlane;

            Vector3 mouseWorldCoordinates = _mainCamera.ScreenToWorldPoint(_mouseInput);

            transform.position = Vector3.MoveTowards(transform.position, mouseWorldCoordinates, Time.deltaTime * speed);

            // Rotation
            if (mouseWorldCoordinates != transform.position)
            {
                // Rotate in the direction of our target (the mouse pointer position)
                Vector3 targetDirection = mouseWorldCoordinates - transform.position;
                targetDirection.z = 0;
                transform.up = targetDirection;
            }
        }
    }
}
