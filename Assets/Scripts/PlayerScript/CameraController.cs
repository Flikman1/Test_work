using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float mouseSensitivity = 100f; // Чувствительность мыши
    public Transform playerBody;         // Ссылка на тело игрока

    private float xRotation = 0f;

    void Start()
    {
        // Скрываем курсор и фиксируем его в центре экрана
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Получаем движение мыши
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Ограничиваем вращение по оси X (вверх-вниз)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Вращаем камеру вверх-вниз
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Вращаем тело игрока влево-вправо
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
