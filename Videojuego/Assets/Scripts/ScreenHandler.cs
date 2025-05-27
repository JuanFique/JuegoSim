using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenChange : MonoBehaviour
{
    public string[] sceneNames;

    public void ChangeScene(string sceneName)
    {
        Time.timeScale = 1f; // ✅ Reanudar tiempo antes de cargar
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
