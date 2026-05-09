using UnityEngine;

public class Fam : MonoBehaviour
{
    [Header("References")]
    public Transform cameraPivot;

    [Header("Mouse Settings")]
    public float sensitivity = 150f;
    public float minX = -85f;
    public float maxX = 85f;

    float xRotation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        HandleMouseLook();
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * sensitivity * Time.deltaTime;

        transform.Rotate(Vector3.up * mouseX);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, minX, maxX);

        cameraPivot.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}