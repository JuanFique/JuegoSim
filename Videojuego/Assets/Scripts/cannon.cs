using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cañon : MonoBehaviour
{
    public GameObject cannon;
    public GameObject Bala;
    private Camera cam;
    public float Fuerza = 10f;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        Vector2 mouseWorldPoint = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mouseWorldPoint - (Vector2)transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle = Mathf.Clamp(angle, 0f, 90f);
        transform.rotation = Quaternion.Euler(0, 0, angle);

        if (Input.GetMouseButtonDown(0))
        {
            GameObject balaS = Instantiate(Bala, cannon.transform.position, Quaternion.identity);
            Vector2 direccionDisparo = direction.normalized;

            balaS.GetComponent<Circulo>().Inicializar(direccionDisparo, Fuerza);
        }
    }
}
