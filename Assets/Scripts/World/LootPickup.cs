using UnityEngine;

public class LootPickup : MonoBehaviour
{
    [SerializeField] private ItemSO item;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player")) return;
        Debug.Log($"Coletou: {item.itemName}");
        Destroy(gameObject);
    }
}