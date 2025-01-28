using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerBody;  
    public float mouseSensitivity = 200f;  
    private float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;  // Блокируем курсор в центре экрана
    }

    void Update()
    {
        // Убираем Time.deltaTime для мыши, но оставляем множитель чувствительности
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity * 0.02f; 
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity * 0.02f; 

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);  // Ограничиваем вращение вверх/вниз

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
