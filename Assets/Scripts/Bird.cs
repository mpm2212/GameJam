using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] private float speed = -2f;
    private Rigidbody2D birgRigidbody;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        birgRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveBird();
    }

    void MoveBird()
    {
        Vector2 velocity = birgRigidbody.linearVelocity;
        velocity.x = speed; 
        birgRigidbody.linearVelocity = velocity;
    }
}
