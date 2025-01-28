using UnityEngine;

public class PickupObject : MonoBehaviour
{
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); // Получаем Rigidbody предмета
    }

    public void Pickup(Transform holdPosition)
    {
        Debug.Log("Поднимаем предмет: " + gameObject.name);
        rb.isKinematic = true; // Отключаем физику
        transform.SetParent(holdPosition);
        transform.localPosition = Vector3.zero;
    }

    public void Drop()
    {
        Debug.Log("Бросаем предмет: " + gameObject.name);
        rb.isKinematic = false; // Включаем физику
        transform.SetParent(null);
    }
}
