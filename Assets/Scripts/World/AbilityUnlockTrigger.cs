using UnityEngine;

public class AbilityUnlockTrigger : MonoBehaviour
{
    [SerializeField] private AbilitySO  abilityToUnlock;
    [SerializeField] private DialogueSO unlockDialogue;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player")) return;
        abilityToUnlock.isUnlocked = true;
        if (unlockDialogue != null)
            DialogueManager.Instance.StartDialogue(unlockDialogue);
        gameObject.SetActive(false);
    }
}