using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Circulo : MonoBehaviour
{
    public InputField massInput;
    public InputField velocityInput;
    public GameObject ballPrefab;
    public Transform shootPoint;

    public void LaunchBall()
    {
        float mass = float.Parse(massInput.text);
        float velocity = float.Parse(velocityInput.text);

        GameObject ball = Instantiate(ballPrefab, shootPoint.position, Quaternion.identity);
        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
        rb.mass = mass;
        rb.velocity = shootPoint.right * velocity;
    }
}

