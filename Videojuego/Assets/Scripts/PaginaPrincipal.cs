using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PaginaPrincipal : MonoBehaviour
{
    [SerializeField] private AudioSource buttonClickSound;

    public void JugarJuego()
    {
        ReproducirSonido();
        SceneManager.LoadSceneAsync(1);
    }

    public void QuitGame()
    {
        ReproducirSonido();
        Application.Quit();
    }

    private void ReproducirSonido()
    {
        if (buttonClickSound != null)
        {
            buttonClickSound.Play();
        }
        else
        {
            Debug.LogWarning("No se asignó ningún AudioSource en PaginaPrincipal.");
        }
    }
}