using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Transform holdPoint; // Точка, где игрок "держит" объект
    public Transform storagePoint; // Точка в багажнике
    public float moveSpeed = 5f;   // Скорость перемещения

    private bool isHeld = false;  // Указывает, находится ли объект в руках
    private bool isStored = false; // Указывает, был ли объект уже положен в багажник

    private void Update()
    {
        // Если объект в руках, следуем за точкой удержания
        if (isHeld && holdPoint != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, holdPoint.position, moveSpeed * Time.deltaTime);
            transform.rotation = holdPoint.rotation; // Совмещаем ориентацию с точкой удержания
        }
    }

    public void Interact()
    {
        if (isStored)
        {
            Debug.Log("Этот объект уже в багажнике.");
            return;
        }

        if (!isHeld) // Если объект не в руках
        {
            PickUp(); // Поднять объект
        }
        else // Если объект в руках
        {
            Store(); // Положить объект в багажник
        }
    }

    private void PickUp()
    {
        Debug.Log("Игрок взял объект: " + gameObject.name);

        isHeld = true; // Отмечаем объект как "в руках"
        
        // Отключаем гравитацию, если объект имеет Rigidbody
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }
    }

    private void Store()
    {
        Debug.Log("Игрок положил объект в багажник: " + gameObject.name);

        isHeld = false;    // Убираем объект из рук
        isStored = true;   // Отмечаем, что объект положен

        // Перемещаем объект в багажник
        transform.SetParent(storagePoint);
        transform.position = storagePoint.position; // Ставим объект в точку хранения
        transform.rotation = storagePoint.rotation;

        // Можно также отключить физику, если объект уже не нужен для взаимодействия
    }
}
