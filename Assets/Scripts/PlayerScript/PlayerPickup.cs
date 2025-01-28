using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    public float pickupRange = 3f;        // Дистанция взаимодействия
    public Transform holdPosition;       // Позиция, где будет держаться предмет
    private GameObject currentObject;    // Текущий предмет, который игрок держит

    void Update()
    {
        // Взаимодействие по нажатию E
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentObject == null)
            {
                TryPickup(); // Попробовать взять предмет
            }
            else
            {
                DropObject(); // Сбросить предмет
            }
        }
    }

    void TryPickup()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, pickupRange))
        {
            if (hit.transform.CompareTag("Pickup"))
            {
                currentObject = hit.transform.gameObject;
                currentObject.GetComponent<Rigidbody>().isKinematic = true; // Отключить физику
                currentObject.transform.SetParent(holdPosition);           // Привязать к игроку
                currentObject.transform.localPosition = Vector3.zero;     // Центрировать в позиции удержания
                Debug.Log("Взяли объект: " + currentObject.name);
            }
        }
    }

    void DropObject()
    {
        if (currentObject != null)
        {
            currentObject.GetComponent<Rigidbody>().isKinematic = false; // Включить физику
            currentObject.transform.SetParent(null);                    // Убрать из родителя
            currentObject = null;                                       // Сбросить текущий объект
            Debug.Log("Объект сброшен");
        }
    }
}
