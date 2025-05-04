using UnityEngine;

public class LootController : MonoBehaviour
{
    [SerializeField]
    Item Item;

    [SerializeField]
    float RotationSpeed = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (GameController.Instance.Inventory.Contains(Item))
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(Vector3.up, RotationSpeed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameController.Instance.Inventory.AddItem(Item);
            Destroy(gameObject);
        }
    }
}
