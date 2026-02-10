using UnityEngine;

namespace ZooArchitect.View.Scene
{
    internal sealed class CameraView : ViewComponent
    {
#if MOUSE_CAMERA_CONTROLS
        private const bool MOUSE_CONTROLS = true;
#else
        private const bool MOUSE_CONTROLS = false;
#endif

        private const float MOVE_SPEED = 15.0f;
        private const float SMOOTH_TIME = 0.15f;
        private const float EDGE_SIZE = 20.0f;

        private Vector3 velocity;
        private Vector3 targetVelocity;

        private Camera gameCamera;

        public Camera GameCamera => gameCamera;

        public override void Init(params object[] parameters)
        {
            base.Init(parameters);
            gameCamera = parameters[0] as Camera;
            GameCamera.transform.position = Vector3.back;
        }

        public override void Tick(float deltaTime)
        {
            Vector3 mousePosition = Input.mousePosition;
            Vector3 direction = Vector3.zero;

            if (Input.GetKey(KeyCode.LeftArrow) || (MOUSE_CONTROLS && mousePosition.x <= EDGE_SIZE))
                direction.x = -1.0f;
            else if (Input.GetKey(KeyCode.LeftArrow) || (MOUSE_CONTROLS && mousePosition.x >= (Screen.width - EDGE_SIZE)))
                direction.x = 1.0f;

            if (Input.GetKey(KeyCode.LeftArrow) || (MOUSE_CONTROLS && mousePosition.y <= EDGE_SIZE))
                direction.y = -1.0f;
            else if (Input.GetKey(KeyCode.LeftArrow) || (MOUSE_CONTROLS && mousePosition.y >= (Screen.height - EDGE_SIZE)))
                direction.y = 1.0f;

            if (direction.sqrMagnitude > 0.0f)
            {
                direction.Normalize();
                targetVelocity += direction * MOVE_SPEED * deltaTime;
            }

            targetVelocity.z = 0.0f; 
            Vector3 newPos = Vector3.SmoothDamp(transform.position, targetVelocity, ref velocity, SMOOTH_TIME);
            newPos.z = -10;
            transform.position = newPos;
        }
    }
}
