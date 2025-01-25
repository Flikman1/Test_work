using UnityEngine;

public class GateController : MonoBehaviour
{
    public Animator animator; // Ссылка на Animator ворот
    private bool isOpen = false; // Состояние ворот (открыты/закрыты)

    public void Interact()
    {
        if (!isOpen)
        {
            OpenGate();
        }
        else
        {
            CloseGate();
        }
    }

    private void OpenGate()
    {
        Debug.Log("Открытие ворот...");
        isOpen = true; // Обновляем состояние ворот
        animator.SetTrigger("OpenGate"); // Запускаем анимацию открытия
    }

    private void CloseGate()
    {
        Debug.Log("Закрытие ворот...");
        isOpen = false; // Обновляем состояние ворот
        animator.SetTrigger("CloseGate"); // Запускаем анимацию закрытия (если есть)
    }
}
