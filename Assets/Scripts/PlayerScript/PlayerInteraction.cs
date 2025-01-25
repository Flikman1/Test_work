using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionRange = 3f; // Дистанция взаимодействия
    public Camera playerCamera;        // Камера игрока
    public Image crosshair;            // UI-элемент прицела

    private Interactable currentInteractable = null; // Компонент Interactable
    private GateController currentGate = null;       // Компонент GateController

    private void Update()
    {
        // Проверяем, наведён ли прицел на объект
        CheckForInteractable();

        // Если нажата кнопка взаимодействия
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentInteractable != null)
            {
                currentInteractable.Interact(); // Взаимодействие с объектами типа Interactable
            }
            else if (currentGate != null)
            {
                currentGate.Interact(); // Взаимодействие с воротами
            }
        }
    }

    private void CheckForInteractable()
    {
        // Луч от центра камеры
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        // Если луч попадает в объект
        if (Physics.Raycast(ray, out hit, interactionRange))
        {
            // Проверяем, есть ли у объекта компонент Interactable
            Interactable interactable = hit.transform.GetComponent<Interactable>();
            GateController gate = hit.transform.GetComponent<GateController>();

            if (interactable != null) // Если найден Interactable объект
            {
                if (currentInteractable != interactable)
                {
                    currentInteractable = interactable;
                    currentGate = null; // Обнуляем ссылку на ворота
                    crosshair.color = Color.green; // Прицел становится зелёным
                }
                return;
            }
            else if (gate != null) // Если найден объект ворот
            {
                if (currentGate != gate)
                {
                    currentGate = gate;
                    currentInteractable = null; // Обнуляем ссылку на Interactable
                    crosshair.color = Color.green; // Прицел становится зелёным
                }
                return;
            }
        }

        // Если ни Interactable, ни GateController не найдены
        currentInteractable = null;
        currentGate = null;
        crosshair.color = Color.white; // Возвращаем цвет прицела
    }
}
