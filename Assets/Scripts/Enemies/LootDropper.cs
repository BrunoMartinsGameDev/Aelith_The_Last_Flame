using UnityEngine;

public class LootDropper : MonoBehaviour
{
    [SerializeField] private GameObject lootPrefab;

    public void Drop(LootTableSO table)
    {
        if (table == null || lootPrefab == null) return;

        var item = table.Roll();
        if (item == null) return;
        
        var dropped = Instantiate(lootPrefab, transform.position+Vector3.up * 0.5f, Quaternion.identity);

        dropped.GetComponent<LootPickup>().SetItem(item);
    }
}