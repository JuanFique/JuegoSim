using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cañon : MonoBehaviour
{
    public GameObject cannon;
    public GameObject Bala;
    private Camera cam;
    public float Fuerza = 10f;
[SerializeField] private float _initialVelocity = 10f;
[SerializeField] private Vector2 Gravity = new Vector2(0, -9.8f);
[SerializeField] private GameObject _predictPoint; 
[SerializeField] private int _predictionBallCount = 30;
private GameObject[] _predictionPointArray;
[SerializeField] private Transform SpawnPoint;
[SerializeField] private float viento = 1f;



    void Start()
    {
        cam = Camera.main;

         _predictionPointArray = new GameObject[_predictionBallCount];
        for (int i = 0; i < _predictionBallCount; i++)
        {
            _predictionPointArray[i] = Instantiate(_predictPoint, SpawnPoint.position, Quaternion.identity); // ref https://www.youtube.com/watch?v=rqhAOc9gvC4

        }
    }

void Update()
{
    Vector2 mouseWorldPoint = cam.ScreenToWorldPoint(Input.mousePosition);
    Vector2 direction = mouseWorldPoint - (Vector2)SpawnPoint.position;  

    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    angle = Mathf.Clamp(angle, 0f, 90f);
    cannon.transform.rotation = Quaternion.Euler(0, 0, angle);  

    UpdatePrediction(direction.normalized);

    if (Input.GetMouseButtonDown(0))
    {
        GameObject balaS = Instantiate(Bala, SpawnPoint.position, Quaternion.identity);
        balaS.GetComponent<Circulo>().Inicializar(direction.normalized, Fuerza);
    }
}



private void UpdatePrediction(Vector2 aimDirection)
{
    Vector2 startPosition = SpawnPoint.position;
    Vector2 velocity = aimDirection * _initialVelocity;

    Vector2 acceleration = new Vector2(viento, Gravity.y); 

    float step = 0.05f;
    float t = 0f;
    int i = 0;

    for (; i < _predictionPointArray.Length; i++)
    {
        Vector2 pos = 0.5f * acceleration * t * t + velocity * t + startPosition;

        if (pos.y <= -3.35f) 
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




}
