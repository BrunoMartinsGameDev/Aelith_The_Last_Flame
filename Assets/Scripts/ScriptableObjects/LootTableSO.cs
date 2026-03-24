using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLootTable", menuName = "Game/Loot Table")]
public class LootTableSO : ScriptableObject
{
    [Serializable]
    public struct LootEntry
    {
        public ItemSO item;
        [Range(0f, 1f)]
        public float dropChance;
    }

    public LootEntry[] entries;

    public ItemSO Roll()
    {
        foreach (var entry in entries)
            if (UnityEngine.Random.value <= entry.dropChance)
                return entry.item;
        return null;
    }
}