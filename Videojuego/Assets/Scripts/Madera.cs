using UnityEngine;

public class Madera : MonoBehaviour
{
    public Circulo circulo;

    public float energiaRebote = 0.6f;

    private float left, right, top, bottom;

    void Start()
    {
        left = transform.position.x - transform.localScale.x / 2f;
        right = transform.position.x + transform.localScale.x / 2f;
        top = transform.position.y + transform.localScale.y / 2f;
        bottom = transform.position.y - transform.localScale.y / 2f;
    }

    void Update()
    {
        if (circulo == null) return;

        Vector2 posCirculo = circulo.transform.position;
        float radio = circulo.radio;

        bool colisionX = posCirculo.x + radio > left && posCirculo.x - radio < right;
        bool colisionY = posCirculo.y - radio < top && posCirculo.y + radio > bottom;

        if (colisionX && colisionY)
        {
            if (posCirculo.y - radio < bottom)
            {
                Vector3 posAjustada = circulo.transform.position;
                posAjustada.y = bottom + radio;
                circulo.transform.position = posAjustada;
            }

            circulo.velocidad = new Vector2(
                circulo.velocidad.x * 0.8f,
                -circulo.velocidad.y * energiaRebote
            );

            if (Mathf.Abs(circulo.velocidad.y) < 0.1f)
                circulo.velocidad = new Vector2(circulo.velocidad.x, 0);
        }
    }
}
