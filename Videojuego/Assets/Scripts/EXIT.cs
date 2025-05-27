using UnityEngine;

public class SalirDelJuego : MonoBehaviour
{
    public void Salir()
    {
        Debug.Log("Cerrando el juego...");

#if UNITY_EDITOR
        // Si estás en el editor de Unity, solo detiene el modo Play
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // En un ejecutable de PC (.exe), cierra el juego
        Application.Quit();
#endif
    }
}
