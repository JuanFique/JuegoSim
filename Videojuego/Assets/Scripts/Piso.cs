using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piso : MonoBehaviour
{
    private Rigidbody rb;
    private float alturaMinima = -8.3f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (transform.position.y < alturaMinima)
        {
            Vector3 newPos = transform.position;
            newPos.y = alturaMinima + GetComponent<Collider>().bounds.extents.y;
            transform.position = newPos;

            if (rb != null)
            {
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            }
        }
    }
}
