using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cañon : MonoBehaviour
{
    public GameObject cannon;
    public GameObject Bala;
    private Camera cam;

    private float fuerzaActual = 5f;
    private bool cargandoFuerza = false;
    private float tiempoInicioCarga = 0f;

    [SerializeField] private float _initialVelocity = 10f;
    [SerializeField] private Vector2 Gravity = new Vector2(0, -9.8f);
    [SerializeField] private GameObject _predictPoint;
    [SerializeField] private int _predictionBallCount = 30;
    private GameObject[] _predictionPointArray;
    [SerializeField] private Transform SpawnPoint;
    [SerializeField] private float viento = 1f;

    private float fuerzaMin = 5f;
    private float fuerzaMax = 30f; // límite antes de reiniciar
    private float velocidadCarga = 15f;

    void Start()
    {
        cam = Camera.main;

        _predictionPointArray = new GameObject[_predictionBallCount];
        for (int i = 0; i < _predictionBallCount; i++)
        {
            _predictionPointArray[i] = Instantiate(_predictPoint, SpawnPoint.position, Quaternion.identity);
            _predictionPointArray[i].SetActive(false); // los ocultamos por defecto
        }
    }

    void Update()
    {
        Vector2 mouseWorldPoint = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mouseWorldPoint - (Vector2)SpawnPoint.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle = Mathf.Clamp(angle, 0f, 90f);
        cannon.transform.rotation = Quaternion.Euler(0, 0, angle);

        // Inicia la carga
        if (Input.GetMouseButtonDown(0))
        {
            cargandoFuerza = true;
            tiempoInicioCarga = Time.time;
        }

        // Mientras se mantiene presionado, calcula fuerza en función del tiempo y muestra la predicción
        if (cargandoFuerza && Input.GetMouseButton(0))
        {
            float tiempoCargando = Time.time - tiempoInicioCarga;
            fuerzaActual = fuerzaMin + (tiempoCargando * velocidadCarga);

            if (fuerzaActual > fuerzaMax)
            {
                fuerzaActual = fuerzaMin;
                tiempoInicioCarga = Time.time; // reinicia el tiempo también
            }

            UpdatePrediction(direction.normalized, fuerzaActual);
        }

        // Al soltar el clic, se lanza la bala y se oculta la predicción
        if (cargandoFuerza && Input.GetMouseButtonUp(0))
        {
            cargandoFuerza = false;

            GameObject balaS = Instantiate(Bala, SpawnPoint.position, Quaternion.identity);
            balaS.GetComponent<Circulo>().Inicializar(direction.normalized, fuerzaActual);

            // Oculta todos los puntos
            for (int i = 0; i < _predictionPointArray.Length; i++)
            {
                _predictionPointArray[i].SetActive(false);
            }
        }
    }

    private void UpdatePrediction(Vector2 aimDirection, float fuerza)
    {
        Vector2 startPosition = SpawnPoint.position;
        float angle = Vector2.Angle(Vector2.right, aimDirection); // mide ángulo respecto a horizontal
        float step = 0.08f;

        if (angle >= 30f)
        {
            // --- Predicción parabólica (fórmula cerrada) ---
            Vector2 velocity = aimDirection * fuerza;
            Vector2 acceleration = new Vector2(viento, Gravity.y);
            float t = 0f;
            int i = 0;

            for (; i < _predictionPointArray.Length; i++)
            {
                Vector2 pos = 0.5f * acceleration * t * t + velocity * t + startPosition;

                if (pos.y <= -10f)
                    break;

                _predictionPointArray[i].transform.position = pos;
                _predictionPointArray[i].SetActive(true);

                t += step;
            }

            for (; i < _predictionPointArray.Length; i++)
            {
                _predictionPointArray[i].SetActive(false);
            }
        }
        else
        {
            // --- Predicción por simulación paso a paso ---
            Vector2 position = startPosition;
            Vector2 velocity = aimDirection * fuerza;
            Vector2 gravity = new Vector2(viento, Gravity.y);

            int i = 0;
            for (; i < _predictionPointArray.Length; i++)
            {
                _predictionPointArray[i].transform.position = position;
                _predictionPointArray[i].SetActive(true);

                velocity += gravity * step;
                position += velocity * step;

                if (position.y < -10f)
                    break;
            }

            for (; i < _predictionPointArray.Length; i++)
            {
                _predictionPointArray[i].SetActive(false);
            }
        }
    }
}