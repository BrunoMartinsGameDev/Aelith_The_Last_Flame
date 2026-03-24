using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    [SerializeField] private DialogueUI ui;

    private DialogueSO _current;
    private int        _lineIndex;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
    }

    public void StartDialogue(DialogueSO dialogue)
    {
        if (dialogue == null || dialogue.hasBeenRead) return;
        _current   = dialogue;
        _lineIndex = 0;
        PlayerController.Instance.SetInputEnabled(false);
        ui.Show(_current.npcName, _current.portrait, _current.lines[0]);
    }

    public void NextLine()
    {
        _lineIndex++;
        if (_lineIndex >= _current.lines.Length) { EndDialogue(); return; }
        ui.UpdateLine(_current.lines[_lineIndex]);
    }

    private void EndDialogue()
    {
        _current.hasBeenRead = true;
        PlayerController.Instance.SetInputEnabled(true);
        ui.Hide();
    }
}