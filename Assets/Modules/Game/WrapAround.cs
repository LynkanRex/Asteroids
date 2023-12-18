using UnityEngine;

namespace Modules.Game
{
    public class WrapAround : MonoBehaviour
    {
        private Camera mainCamera;
        private float halfWidth;
        private float halfHeight;

        private void Start()
        {
            mainCamera = Camera.main;

            // Calculate half of the camera's width and height in world coordinates
            if (mainCamera == null) return;
            
            halfWidth = mainCamera.orthographicSize * mainCamera.aspect;
            halfHeight = mainCamera.orthographicSize;
        }

        private void Update()
        {
            if (!mainCamera) return;
            
            if (transform.position.x < mainCamera.transform.position.x - halfWidth)
            {
                transform.position = new Vector2(mainCamera.transform.position.x + halfWidth, transform.position.y);
            }
            else if (transform.position.x > mainCamera.transform.position.x + halfWidth)
            {
                transform.position = new Vector2(mainCamera.transform.position.x - halfWidth, transform.position.y);
            }

            if (transform.position.y < mainCamera.transform.position.y - halfHeight)
            {
                transform.position = new Vector2(transform.position.x, mainCamera.transform.position.y + halfHeight);
            }
            else if (transform.position.y > mainCamera.transform.position.y + halfHeight)
            {
                transform.position = new Vector2(transform.position.x, mainCamera.transform.position.y - halfHeight);
            }
        }
    }
}
