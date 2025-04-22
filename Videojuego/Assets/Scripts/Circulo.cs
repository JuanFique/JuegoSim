using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Circulo : MonoBehaviour
{
    public Vector2 direccion;
    public float velocidad;

    public void Inicializar(Vector2 dir, float vel)
    {
        direccion = dir;
        velocidad = vel;
    }   

    void Update()
    {
        transform.position += (Vector3)(direccion * velocidad * Time.deltaTime);
    }
}


