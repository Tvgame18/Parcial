using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [Header("Mouse")]
    [SerializeField]
    private float mouseSensitivity = 200f;

    [SerializeField]
    private Transform player;

    [Header("FOV")]
    [SerializeField]
    private float minFov = 50f;

    [SerializeField]
    private float maxFov = 120f;

    [SerializeField]
    private float fovChange = 5f;

    private float verticalRotation;

    private Camera playerCamera;

    private void Start()
    {
        playerCamera = GetComponent<Camera>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        Look();
        ChangeFov();
    }

    private void Look()
    {
        float mouseX = Input.GetAxis("Mouse X")
                       * mouseSensitivity
                       * Time.deltaTime;

        float mouseY = Input.GetAxis("Mouse Y")
                       * mouseSensitivity
                       * Time.deltaTime;

        // Girar el Player horizontalmente
        player.Rotate(Vector3.up * mouseX);

        // Girar la cámara verticalmente
        verticalRotation -= mouseY;

        verticalRotation = Mathf.Clamp(
            verticalRotation,
            -90f,
            90f
        );

        transform.localRotation = Quaternion.Euler(
            verticalRotation,
            0f,
            0f
        );
    }

    private void ChangeFov()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            playerCamera.fieldOfView -= fovChange;
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            playerCamera.fieldOfView += fovChange;
        }

        playerCamera.fieldOfView = Mathf.Clamp(
            playerCamera.fieldOfView,
            minFov,
            maxFov
        );
    }
}