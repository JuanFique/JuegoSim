using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cañon : MonoBehaviour
{
    public GameObject cannon;
    public GameObject Bala;
    private Camera cam;
    public float Fuerza = 10f;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mouseWorldPoint = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mouseWorldPoint - (Vector2)transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle = Mathf.Clamp(angle, 0f, 90f); // Limita el ángulo entre 0 y 90 grados

        transform.rotation = Quaternion.Euler(0, 0, angle);
        if (Input.GetMouseButton(0))
        {
            GameObject ObjetoBala = Instantiate(Bala);
            ObjetoBala.transform.position = cannon.transform.position;
        }

        if (Input.GetMouseButtonDown(0))
        {
            GameObject balaS = Instantiate(Bala, cannon.transform.position, Quaternion.identity);
            Vector2 direccionDisparo = direction.normalized;

            // Asignar dirección y velocidad directamente a la bala
            balaS.AddComponent<Circulo>().Inicializar(direccionDisparo, Fuerza);
        }
    }
}


    
