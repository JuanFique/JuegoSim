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

    [SerializeField] private float _initialVelocity = 10f;
    [SerializeField] private Vector2 Gravity = new Vector2(0, -9.8f);
    [SerializeField] private GameObject _predictPoint;
    [SerializeField] private int _predictionBallCount = 30;
    private GameObject[] _predictionPointArray;
    [SerializeField] private Transform SpawnPoint;
    [SerializeField] private float viento = 1f;
    [SerializeField] private AudioSource shootAudioSource;

    private float fuerzaMin = 5f;
    private float fuerzaMax = 20f;
    private float velocidadCarga = 10f;

    void Start()
    {
        cam = Camera.main;

        _predictionPointArray = new GameObject[_predictionBallCount];
        for (int i = 0; i < _predictionBallCount; i++)
        {
            _predictionPointArray[i] = Instantiate(_predictPoint, SpawnPoint.position, Quaternion.identity);
            _predictionPointArray[i].SetActive(false);
        }

        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }

    private void OnDestroy()
    {
        GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
    }

    void Update()
    {
        Vector2 mouseWorldPoint = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mouseWorldPoint - (Vector2)SpawnPoint.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle = Mathf.Clamp(angle, 0f, 90f);
        cannon.transform.rotation = Quaternion.Euler(0, 0, angle);

        // Inicia carga de fuerza
        if (Input.GetMouseButtonDown(0))
        {
            cargandoFuerza = true;
            fuerzaActual = fuerzaMin;
        }

        // Mientras se mantiene presionado
        if (cargandoFuerza && Input.GetMouseButton(0))
        {
            fuerzaActual += velocidadCarga * Time.deltaTime;
            if (fuerzaActual > fuerzaMax)
                fuerzaActual = fuerzaMin;

            UpdatePrediction(direction.normalized, fuerzaActual);
        }
        else
        {
            // Si no se está cargando, oculta puntos
            for (int i = 0; i < _predictionPointArray.Length; i++)
                _predictionPointArray[i].SetActive(false);
        }

        // Al soltar el clic, disparar
        if (cargandoFuerza && Input.GetMouseButtonUp(0))
        {
            cargandoFuerza = false;

            GameObject balaS = Instantiate(Bala, SpawnPoint.position, Quaternion.identity);
            Circulo circ = balaS.GetComponent<Circulo>();
            if (circ != null)
            {
                circ.Inicializar(direction.normalized, fuerzaActual);
            }

            if (shootAudioSource != null)
                shootAudioSource.Play();
        }
    }

    private void UpdatePrediction(Vector2 aimDirection, float fuerza)
    {
        Vector2 startPosition = SpawnPoint.position;
        float angle = Vector2.Angle(Vector2.right, aimDirection);
        float step = 0.08f;
        int i = 0;

        if (angle >= 30f)
        {
            Vector2 velocity = aimDirection * fuerza;
            Vector2 acceleration = new Vector2(viento, Gravity.y);
            float t = 0f;

            for (; i < _predictionPointArray.Length; i++)
            {
                Vector2 pos = 0.5f * acceleration * t * t + velocity * t + startPosition;
                if (pos.y <= -10f) break;

                _predictionPointArray[i].transform.position = pos;
                t += step;
            }
        }
        else
        {
            Vector2 position = startPosition;
            Vector2 velocity = aimDirection * fuerza;
            Vector2 gravity = new Vector2(viento, Gravity.y);

            for (; i < _predictionPointArray.Length; i++)
            {
                _predictionPointArray[i].transform.position = position;
                velocity += gravity * step;
                position += velocity * step;

                if (position.y < -10f) break;
            }
        }

        // Activar solo los puntos usados
        for (int j = 0; j < i; j++)
            _predictionPointArray[j].SetActive(true);

        // Desactivar los que sobran
        for (int j = i; j < _predictionPointArray.Length; j++)
            _predictionPointArray[j].SetActive(false);
    }

    private void OnGameStateChanged(GameState newGameState)
    {
        enabled = newGameState == GameState.Gameplay;
    }
}
