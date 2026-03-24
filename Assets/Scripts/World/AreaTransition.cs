using UnityEngine;

public class AreaTransition : MonoBehaviour
{
    [SerializeField] private string    targetScene;
    [SerializeField] private AbilitySO requiredAbility;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player")) return;
        if (requiredAbility != null && !requiredAbility.isUnlocked)
        {
            Debug.Log("Habilidade necessária não desbloqueada");
            return;
        }
        SceneLoader.Instance.LoadScene(targetScene);
    }
}