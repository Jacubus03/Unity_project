using UnityEngine;

public class CameraPlayer : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform cameraTransform; // Assign the player's transform (not the camera)
    public Rigidbody rigidbodyPlayer;

    float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Hide and lock the cursor
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Prevent full vertical rotation

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // Vertical rotation (camera only)
        transform.Rotate(Vector3.up * mouseX); // Horizontal rotation (rotate player)
    }
}
