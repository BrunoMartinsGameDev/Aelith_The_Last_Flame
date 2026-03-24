using UnityEngine;

[CreateAssetMenu(fileName = "NewAbility", menuName = "Game/Ability")]
public class AbilitySO : ScriptableObject
{
    public string abilityName;
    [TextArea(1, 3)]
    public string description;
    public Sprite icon;
    public bool   isUnlocked;
}