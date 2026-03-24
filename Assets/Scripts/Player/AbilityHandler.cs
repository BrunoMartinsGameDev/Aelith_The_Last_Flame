using UnityEngine;

public class AbilityHandler : MonoBehaviour
{
    [SerializeField] private AbilitySO dashAbility;
    [SerializeField] private AbilitySO shieldAbility;

    public bool CanDash   => dashAbility   != null && dashAbility.isUnlocked;
    public bool CanShield => shieldAbility != null && shieldAbility.isUnlocked;
}