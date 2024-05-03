using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float lifespan = 2f; // Adjust this value to change the lifespan of the fireball
     Health health;
    private void Start()
    {
        // Start the countdown to destroy the fireball after the specified lifespan
        Destroy(gameObject, lifespan);
    }
}
