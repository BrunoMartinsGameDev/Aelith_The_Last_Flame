using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private bool _paused;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Toggle();
    }

    private void Toggle()
    {
        _paused        = !_paused;
        Time.timeScale = _paused ? 0f : 1f;
        gameObject.SetActive(_paused);
    }
}