using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circulo : MonoBehaviour
{
    private Vector2 velocidad;
    private Vector2 aceleracion;

    public float gravedad = -9.81f; // Hacia abajo
    public float viento = 1f;       // Hacia la derecha

    public void Inicializar(Vector2 direccionInicial, float fuerza)
    {
        velocidad = direccionInicial * fuerza;
        aceleracion = new Vector2(viento, gravedad); // constante
    }

    void Update()
    {
        velocidad += aceleracion * Time.deltaTime;
        transform.position += (Vector3)(velocidad * Time.deltaTime);
    }
}
