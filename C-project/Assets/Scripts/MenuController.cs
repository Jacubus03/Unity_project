using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class VRMenuUI : MonoBehaviour
{
    public GameObject menuCanvas;

    public InputActionReference openMenuAction;

    void Update()
    {
        if (openMenuAction.action.WasPressedThisFrame())
        {
            ToggleMenu();
        }

        Debug.Log(openMenuAction);
    }

    public void OnContinue()
    {
        menuCanvas.SetActive(false);
        Time.timeScale = 1f;
    }

    public void OnExit()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void ToggleMenu()
    {
        bool active = true;
        menuCanvas.SetActive(active);
        Time.timeScale = active ? 0f : 1f;
    }
}
