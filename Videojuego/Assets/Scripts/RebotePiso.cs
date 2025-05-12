using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RebotePiso : MonoBehaviour
{
    public Transform piso;             // Referencia al piso (asignas en el Inspector)
    public float velocidadY = 0f;
    public float energiaRebote = 0.8f;

    void Update()
    {

        // Mover el objeto
        transform.position += new Vector3(0, velocidadY * Time.deltaTime, 0);

        // Si toca el piso
        if (transform.position.y <= piso.position.y)
        {
            // Corregimos la posición
            transform.position = new Vector3(transform.position.x, piso.position.y, transform.position.z);

            // Rebote
            velocidadY = -velocidadY * energiaRebote;

            // Detener rebote si es muy débil
            if (Mathf.Abs(velocidadY) < 0.1f)
            {
                velocidadY = 0f;
            }
        }
    }
}