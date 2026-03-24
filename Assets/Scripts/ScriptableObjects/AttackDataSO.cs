using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAttack", menuName = "Game/Attack Data")]
public class AttackDataSO : ScriptableObject
{
    [Serializable]
    public struct ComboStep
    {
        public string animationName;
        public int    damage;
        public float  knockbackForce;
        public float  hitboxDuration;
        public float  hitboxRadius;
    }

    [Header("Combo")]
    public ComboStep[] comboSteps;

    [Header("Timing")]
    public float comboResetTime = 0.8f;
    public float cooldown       = 0.3f;
}