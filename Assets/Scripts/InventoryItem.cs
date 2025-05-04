
using UnityEngine;
using UnityEngine.EventSystems;


public class InventoryItem : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    [SerializeField]
    Item Item;
    [SerializeField]
    Transform FreeMovong;
    [SerializeField]
    Transform InventoryBar;

    [SerializeField]
    RectTransform Target;
    bool _isPlaced;
    CompComtroller _comp;
    public void Init(CompComtroller comp)
    {
        _comp = comp;
        gameObject.SetActive(GameController.Instance.Inventory.Contains(Item));
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (_isPlaced) return;
        transform.SetParent(FreeMovong);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_isPlaced) return;
        transform.position = eventData.pointerCurrentRaycast.worldPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_isPlaced) return;
        var distance = Vector2.Distance(Target.position, transform.position);
        Debug.Log("Distance: " + distance);
        if (distance < 2)
        {
            transform.position = Target.position;
            _isPlaced = true;
            _comp.PlaceItem(Item);
        }
        else
        {
            transform.SetParent(InventoryBar);
        }
    }
}
