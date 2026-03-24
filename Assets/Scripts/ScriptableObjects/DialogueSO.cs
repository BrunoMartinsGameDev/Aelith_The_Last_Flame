using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogue", menuName = "Game/Dialogue")]
public class DialogueSO : ScriptableObject
{
    public string   npcName;
    public Sprite   portrait;
    [TextArea(2, 5)]
    public string[] lines;
    [HideInInspector]
    public bool     hasBeenRead;
}