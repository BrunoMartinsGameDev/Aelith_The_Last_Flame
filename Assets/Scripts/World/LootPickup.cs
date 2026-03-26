using UnityEngine;

public class LootPickup : MonoBehaviour
{
    [SerializeField] private ItemSO _item;

    public void SetItem(ItemSO newItem) => _item = newItem;
    void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player")) return;
        if (_item == null) return;
        
        Debug.Log($"Coletou: {_item.itemName}");
        Destroy(gameObject);
    }

}