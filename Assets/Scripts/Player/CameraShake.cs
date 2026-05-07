using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("References")]
    public Transform cameraHolder;
    public Transform cameraPivot;
    public Movement movement;

    [Header("Mouse Settings")]
    public float sensitivity = 150f;
    public float minX = -85f;
    public float maxX = 85f;

    [Header("Head Bob")]
    public float bobFrequency = 6f;
    public float bobAmplitude = 0.05f;
    public float bobSpeedMultiplier = 5f;

    [Header("Noise Shake")]
    public float noiseFrequency = 1f;
    public float noiseAmplitude = 0.01f;

    float xRotation;
    float time;

    Vector3 baseLocalPos;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        if (cameraHolder != null)
        {
            baseLocalPos = cameraHolder.localPosition;
        }
    }

    void Update()
    {
        HandleMouseLook();
        HandleCameraEffects();
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * sensitivity * Time.deltaTime;

        Quaternion yawRotation = Quaternion.Euler(0f, mouseX, 0f);

        movement.rb.MoveRotation(movement.rb.rotation * yawRotation);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, minX, maxX);

        cameraPivot.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    void HandleCameraEffects()
    {
        bool isMoving = movement != null && movement.isMoving;

        Vector3 bobOffset = Vector3.zero;

        if (isMoving)
        {
            time += Time.deltaTime * bobFrequency * bobSpeedMultiplier;

            float y = Mathf.Sin(time) * bobAmplitude;

            bobOffset = new Vector3(0f, y, 0f);
        }
        else
        {
            time = 0f;
        }

        float noiseX = (Mathf.PerlinNoise(Time.time * noiseFrequency, 0f) - 0.5f) * noiseAmplitude;
        float noiseY = (Mathf.PerlinNoise(0f, Time.time * noiseFrequency) - 0.5f) * noiseAmplitude;

        Vector3 noiseOffset = new Vector3(noiseX, noiseY, 0f);

        cameraHolder.localPosition = baseLocalPos + bobOffset + noiseOffset;
    }
}