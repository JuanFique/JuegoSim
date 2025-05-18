using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Madera : MonoBehaviour
{
    [Header("Tamaño de la madera")]
    public float izquierda = -0.5f;
    public float abajo = -0.5f;
    public float ancho = 1f;
    public float alto = 1f;

    [Header("Propiedades físicas")]
    public float masa = 2f;
    public float alturaPiso = -4.0f;

    private Vector2 velocidad;
    private Vector2 fuerzaAcumulada;

    public float gravedad = -9.81f; 

    void Update()
    {
   
        AplicarFuerza(new Vector2(0, gravedad));
        AplicarFisica();
    }


    void AplicarFisica()
    {
        velocidad += (fuerzaAcumulada / masa) * Time.deltaTime;
        fuerzaAcumulada = Vector2.zero;

        Vector2 movimiento = velocidad * Time.deltaTime;
        transform.position += (Vector3)movimiento;

        // Rebote con el piso
        if (transform.position.y <= alturaPiso)
        {
            transform.position = new Vector2(transform.position.x, alturaPiso);
            velocidad.y = -velocidad.y * 0.4f;

            if (Mathf.Abs(velocidad.y) < 0.2f)
            {
                velocidad.y = 0;
            }
        }
    }


    public void AplicarFuerza(Vector2 fuerza)
    {
        fuerzaAcumulada += fuerza;
    }

    public void AplicarImpacto(Vector2 punto, Vector2 fuerza)
    {
        AplicarFuerza(fuerza);
    }

    public bool VerificarColisionPrecisa(Vector2 centro, float radio)
    {
        Rect hitbox = ObtenerRect();
        Vector2 puntoCercano = new Vector2(
            Mathf.Clamp(centro.x, hitbox.xMin, hitbox.xMax),
            Mathf.Clamp(centro.y, hitbox.yMin, hitbox.yMax)
        );
        float distancia = Vector2.Distance(centro, puntoCercano);
        return distancia <= radio;
    }

    Rect ObtenerRect()
    {
        return new Rect(
            transform.position.x + izquierda,
            transform.position.y + abajo,
            ancho,
            alto
        );
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 centro = new Vector3(
            transform.position.x + izquierda + ancho / 2f,
            transform.position.y + abajo + alto / 2f,
            0
        );
        Gizmos.DrawWireCube(centro, new Vector3(ancho, alto, 0));
    }
}
