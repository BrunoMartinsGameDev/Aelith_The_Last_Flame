using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_Text   npcNameText;
    [SerializeField] private TMP_Text   lineText;
    [SerializeField] private Image      portrait;

    public void Show(string npcName, Sprite spr, string firstLine)
    {
        panel.SetActive(true);
        npcNameText.text = npcName;
        lineText.text    = firstLine;
        if (portrait && spr) portrait.sprite = spr;
    }

    public void UpdateLine(string line) => lineText.text = line;
    public void Hide() => panel.SetActive(false);
}