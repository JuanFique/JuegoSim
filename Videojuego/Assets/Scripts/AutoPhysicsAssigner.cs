using UnityEngine;

public class AutoPhysicsAssigner : MonoBehaviour
{
    public PhysicsMaterial2D woodMaterial;
    public PhysicsMaterial2D rockMaterial;
    public PhysicsMaterial2D iceMaterial;

    void Start()
    {
        AssignPhysicsToChildren(transform);
    }

    void AssignPhysicsToChildren(Transform parent)
    {
        foreach (Transform child in parent)
        {
            AssignPhysics(child);
            AssignPhysicsToChildren(child); // Recursividad por si hay más niveles
        }
    }

    void AssignPhysics(Transform obj)
    {
        string name = obj.name.ToLower();

        // Añadir BoxCollider2D si no tiene
        BoxCollider2D collider = obj.GetComponent<BoxCollider2D>();
        if (collider == null)
            collider = obj.gameObject.AddComponent<BoxCollider2D>();

        // Añadir Rigidbody2D si no tiene
        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        if (rb == null)
            rb = obj.gameObject.AddComponent<Rigidbody2D>();

        rb.gravityScale = 1;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        // Asignar materiales y masa según tipo
        if (name.Contains("wood"))
        {
            collider.sharedMaterial = woodMaterial;
            rb.mass = 2f;
        }
        else if (name.Contains("rock") || name.Contains("stone"))
        {
            collider.sharedMaterial = rockMaterial;
            rb.mass = 8f;
        }
        else if (name.Contains("ice"))
        {
            collider.sharedMaterial = iceMaterial;
            rb.mass = 1f;
        }
    }
}
