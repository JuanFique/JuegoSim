using UnityEngine;
using UnityEngine.UI;

public class BotonSonido : MonoBehaviour
{
    [SerializeField] private AudioSource musicaFondo;
    [SerializeField] private Sprite sonidoEncendido;
    [SerializeField] private Sprite sonidoApagado;
    [SerializeField] private Image botonImage;

    private bool estaSonando = true;

    public void CambiarSonido()
    {
        estaSonando = !estaSonando;

        if (estaSonando)
        {
            musicaFondo.Play();
            botonImage.sprite = sonidoEncendido;
        }
        else
        {
            musicaFondo.Pause(); // o Stop()
            botonImage.sprite = sonidoApagado;
        }
    }
}
