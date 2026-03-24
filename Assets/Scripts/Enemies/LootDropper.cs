using UnityEngine;

public class LootDropper : MonoBehaviour
{
    [SerializeField] private GameObject lootPrefab;

    public void Drop(LootTableSO table)
    {
        if (table == null) return;
        var item = table.Roll();
        if (item != null)
            Debug.Log($"Dropped: {item.itemName}");
    }
}