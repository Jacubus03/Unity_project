using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [Header("Reference")]
    public Transform mainCamera;
    public GameObject menuCanvas;

    [Header("Activation Objects")]
    public GameObject UIInteractor;
    public GameObject grapplignController;

    [Header("Inputs")]
    public InputActionReference openMenuAction;

    private bool isActive = false;

    void Update()
    {
        if (openMenuAction.action.WasPressedThisFrame())
        {
            ToggleMenu();
        }
    }

    public void ToggleMenu()
    {
        menuCanvas.transform.position = mainCamera.position + new Vector3(mainCamera.forward.x, 0, mainCamera.forward.z).normalized * 3;
        menuCanvas.transform.rotation = new Quaternion(0, mainCamera.rotation.y, 0, mainCamera.rotation.w);
        
        isActive = !isActive;

        menuCanvas.SetActive(isActive);
        UIInteractor.SetActive(isActive);
        grapplignController.SetActive(!isActive);

        Time.timeScale = isActive ? 0f : 1f;
    }

    public void OnContinue()
    {
        isActive = false;

        menuCanvas.SetActive(false);
        UIInteractor.SetActive(false);
        grapplignController.SetActive(true);

        Time.timeScale = 1f;
    }

    public void OnExit()
    {
        Application.Quit();
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #endif
    }
}
