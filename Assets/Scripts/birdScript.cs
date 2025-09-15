using UnityEngine;

public class birdScript : MonoBehaviour
{
    [SerializeField] private float speed = -2f;
    private Rigidbody2D birdRigidbody;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        birdRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveBird();
    }

    void MoveBird()
    {
        Vector2 velocity = birdRigidbody.linearVelocity;
        velocity.x = speed;
        birdRigidbody.linearVelocity = velocity;
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
