using UnityEngine;
using UnityEngine.EventSystems;

public class DropOnCustomer : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedObj = eventData.pointerDrag;

        if (droppedObj == null) return;

        CoffeeItem coffeeItem = droppedObj.GetComponent<CoffeeItem>();
        if (coffeeItem == null) return;

        Customer customer = GetComponent<Customer>();
        if (customer != null)
        {
            customer.ReceiveOrder(droppedObj);
        }
    }
}
