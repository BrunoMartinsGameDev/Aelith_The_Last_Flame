using UnityEngine;

public enum ItemType   { Consumable, Equipment }
public enum ItemRarity { Common, Uncommon, Rare }

[CreateAssetMenu(fileName = "NewItem", menuName = "Game/Item")]
public class ItemSO : ScriptableObject
{
    public string     itemName;
    [TextArea(1, 3)]
    public string     description;
    public Sprite     icon;
    public ItemType   type;
    public ItemRarity rarity;

    [Header("Effect (consumable)")]
    public int healthRestore;

    [Header("Effect (equipment)")]
    public int bonusAttack;
    public int bonusDefense;
}