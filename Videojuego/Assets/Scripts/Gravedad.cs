using UnityEngine;

public class Gravedad : MonoBehaviour
{
    [Header("Configuración de gravedad")]
    public float gravedad = -9.81f;
    public float limiteInferiorY = -4.3f;
    public float reboteFactor = 0.3f;
    public float friccion = 0.8f;
    public bool rebota = true;

    [HideInInspector] public Vector2 velocidad;

    void Update()
    {
        // Aplicar gravedad
        velocidad.y += gravedad * Time.deltaTime;

        // Mover el objeto
        transform.position += (Vector3)(velocidad * Time.deltaTime);

        // Verificar si toca el "suelo"
        if (transform.position.y <= limiteInferiorY)
        {
            Vector3 pos = transform.position;
            pos.y = limiteInferiorY;
            transform.position = pos;

            if (rebota)
            {
                velocidad.y = -velocidad.y * reboteFactor;
                velocidad.x *= friccion;

                // Si el rebote es muy bajo, detener
                if (Mathf.Abs(velocidad.y) < 0.1f)
                    velocidad.y = 0f;
            }
            else
            {
                velocidad.y = 0f;
            }
        }

        // Detener si la velocidad es muy baja
        if (velocidad.magnitude < 0.01f)
        {
            velocidad = Vector2.zero;
        }
    }

    public void AplicarFuerza(Vector2 fuerza)
    {
        velocidad += fuerza / 1f; // Asume masa 1 para simplificar
    }
}
