using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        GameStateManager.Instance.SetState(GameState.Paused); // Cambia estado a Pausa
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        GameStateManager.Instance.SetState(GameState.Gameplay); // Cambia estado a Gameplay
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        GameStateManager.Instance.SetState(GameState.Gameplay); // Reinicia en modo juego
    }

    public void Home()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Pagina Principal");
        GameStateManager.Instance.SetState(GameState.Menu); // Estado para menú principal
    }
}
