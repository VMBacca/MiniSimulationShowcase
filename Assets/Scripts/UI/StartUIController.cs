// Responsible only for handling user input
// and triggering scene transitions.

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class StartUIController : MonoBehaviour
{
    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void LoadStaticSimulation()
    {
        SceneManager.LoadScene("StaticSimulationScene");
    }

    public void LoadDynamicSimulation()
    {
        SceneManager.LoadScene("DynamicSimulationScene");
    }

    public void QuitApplication()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
