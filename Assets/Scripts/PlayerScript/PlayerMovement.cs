using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller; // Контроллер игрока
    public float speed = 5f;               // Скорость передвижения

    private void Update()
    {
        // Получаем ввод от клавиатуры
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Направление движения
        Vector3 move = transform.right * x + transform.forward * z;

        // Двигаем игрока
        controller.Move(move * speed * Time.deltaTime);
    }
}
