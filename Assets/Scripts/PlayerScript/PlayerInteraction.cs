using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionRange = 3f;  // Дистанция взаимодействия
    public Camera playerCamera;          // Камера игрока
    public Image crosshair;              // UI-элемент прицела
    public Transform holdPosition;       // Где держится предмет
    public Transform storageZone;        // Багажник пикапа

    private PickupObject currentPickup = null;  // Объект, который можно взять
    private GateController currentGate = null;  // Объект ворот
    private GameObject heldObject = null;       // Предмет, который держит игрок

    private void Update()
    {
        CheckForInteractable();

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentGate != null)
            {
                currentGate.Interact(); // Взаимодействие с воротами
            }
            else if (currentPickup != null)
            {
                if (heldObject == null)
                {
                    PickupItem(currentPickup.gameObject);
                }
                else
                {
                    DropOrStoreItem();
                }
            }
        }
    }

    /// <summary>
    /// Проверяет, наведён ли прицел на объект, с которым можно взаимодействовать
    /// </summary>
    private void CheckForInteractable()
    {
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionRange))
        {
            PickupObject pickup = hit.transform.GetComponent<PickupObject>();
            GateController gate = hit.transform.GetComponent<GateController>();

            if (pickup != null)
            {
                SetCurrentTarget(pickup, null);
                return;
            }
            else if (gate != null)
            {
                SetCurrentTarget(null, gate);
                return;
            }
        }

        SetCurrentTarget(null, null);
    }

    /// <summary>
    /// Устанавливает текущий объект для взаимодействия и меняет цвет прицела
    /// </summary>
    private void SetCurrentTarget(PickupObject pickup, GateController gate)
    {
        currentPickup = pickup;
        currentGate = gate;

        if (pickup != null || gate != null)
        {
            crosshair.color = Color.green; // Меняем цвет прицела
        }
        else
        {
            crosshair.color = Color.white;
        }
    }

    /// <summary>
    /// Поднимаем предмет
    /// </summary>
    private void PickupItem(GameObject item)
    {
        Debug.Log("Взяли предмет: " + item.name);
        heldObject = item;

        Rigidbody rb = heldObject.GetComponent<Rigidbody>();
        rb.isKinematic = true; // Отключаем физику

        heldObject.transform.SetParent(holdPosition);
        heldObject.transform.localPosition = Vector3.zero;
        heldObject.transform.localRotation = Quaternion.identity;
    }

    /// <summary>
    /// Бросаем или кладём предмет в багажник
    /// </summary>
    private void DropOrStoreItem()
    {
        if (heldObject != null)
        {
            if (IsNearStorageZone())
            {
                StoreItem();
            }
            else
            {
                DropItem();
            }
        }
    }

    /// <summary>
    /// Проверяем, рядом ли игрок с багажником
    /// </summary>
    private bool IsNearStorageZone()
    {
        float distance = Vector3.Distance(transform.position, storageZone.position);
        return distance < 2.0f; // Если игрок рядом с багажником
    }

    /// <summary>
    /// Кладём предмет в багажник
    /// </summary>
    private void StoreItem()
    {
        Debug.Log("Предмет " + heldObject.name + " помещён в багажник.");
        heldObject.transform.SetParent(storageZone);
        heldObject.transform.localPosition = Vector3.zero;
        heldObject = null;
    }

    /// <summary>
    /// Бросаем предмет
    /// </summary>
    private void DropItem()
    {
        Debug.Log("Сбросили предмет: " + heldObject.name);
        Rigidbody rb = heldObject.GetComponent<Rigidbody>();

        heldObject.transform.SetParent(null);
        rb.isKinematic = false; // Включаем физику
        rb.AddForce(playerCamera.transform.forward * 2f, ForceMode.Impulse); // Лёгкий бросок вперёд

        heldObject = null;
    }
}
