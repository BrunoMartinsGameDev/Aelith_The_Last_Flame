using UnityEngine;

public class NPCTrigger : MonoBehaviour
{
    [SerializeField] private DialogueSO dialogue;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
            DialogueManager.Instance.StartDialogue(dialogue);
    }
}