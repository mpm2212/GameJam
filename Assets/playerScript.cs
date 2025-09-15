using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class playerScript : MonoBehaviour
{
    public float speed;
    private Rigidbody2D plane;
    private Vector2 movement;
    private SpriteRenderer spriteRenderer;
    public GameObject heartPrefab;
    private GameObject[] hearts;
    private int lives = 3;
    private float heartSpacing = 0.7f;
    private AudioSource music;
    public AudioClip backgroundMusic;
    public GameObject explosionPrefab;
    public bool isDying = false;
    private AudioSource collisionSound;
    public AudioClip birdNoise;
    public AudioClip skydiverNoise;
    public AudioClip explosion;

    void Start()
    {
        plane = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        music = GetComponent<AudioSource>();
        hearts = new GameObject[lives];

        if (music != null && backgroundMusic != null)
        {
            music.clip = backgroundMusic;
            music.loop = true;
            music.Play();
        }

        GameObject livesCounter = new GameObject("Lives");
        Vector2 livesLocation = new Vector2(-25, -25); 
        Vector3 livesCamera = Camera.main.ScreenToWorldPoint(
            new Vector3(Screen.width + livesLocation.x, Screen.height + livesLocation.y, 10)
        );
        livesCounter.transform.position = livesCamera;

        for (int i = 0; i < lives; i++)
        {
            hearts[i] = Instantiate(heartPrefab, livesCounter.transform);
            hearts[i].transform.localPosition = new Vector3(-i * heartSpacing, 0, 0);
        }
    }

    void Update()
    {
        if (!isDying)
        {
            Move();
            UpdateSprite();
        }
    }

    void FixedUpdate()
    {
        plane.linearVelocity = movement * speed;
    }

    void Move()
    {
        movement = Vector2.zero;
        if (Input.GetKey(KeyCode.UpArrow)) movement.y = 1;
        if (Input.GetKey(KeyCode.DownArrow)) movement.y = -1;
        if (Input.GetKey(KeyCode.RightArrow)) movement.x = 1;
        if (Input.GetKey(KeyCode.LeftArrow)) movement.x = -1;
        movement = movement.normalized;
    }

    void UpdateSprite()
    {
        if (movement.y > 0)
        spriteRenderer.transform.rotation = Quaternion.Euler(0, 0, -30f);
        else if (movement.y < 0)
        spriteRenderer.transform.rotation = Quaternion.Euler(0, 0, 30f);
        else
        spriteRenderer.transform.rotation = Quaternion.Euler(0, 0, 0);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (isDying) return; 

        if (other.CompareTag("Skydiver") || other.CompareTag("Bird") )
        {
            TrySpawnExplosionAt(other.transform.position);

            if (collisionSound == null)
            {
                collisionSound = gameObject.AddComponent<AudioSource>();
            }

            if (other.CompareTag("Skydiver") && skydiverNoise != null)
            {
                collisionSound.PlayOneShot(skydiverNoise);
            }
            else if (other.CompareTag("Bird") && birdNoise != null)
            {
                collisionSound.PlayOneShot(birdNoise);
            }
            lives--;
            if (lives >= 0 && hearts[lives] != null)
            {
                StartCoroutine(HeartDisappear(hearts[lives]));
                hearts[lives] = null;
            }
            Destroy(other.gameObject);
            if (lives <= 0)
            {
                collisionSound.PlayOneShot(explosion);
                StartCoroutine(Restart());
            }
        }

        if (other.CompareTag("Border"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    private IEnumerator HeartDisappear(GameObject heart)
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(heart);
    }

    private IEnumerator Restart()
    {
        isDying = true;
        DistanceText dt = FindObjectOfType<DistanceText>();
        if (dt != null)
        {
            dt.enabled = false;
        }
        movement = Vector2.zero;
        plane.linearVelocity = Vector2.zero;

        float waitTime = TrySpawnExplosionAt(transform.position);

        if (spriteRenderer != null) spriteRenderer.enabled = false;
        Collider2D col = GetComponent<BoxCollider2D>();
        if (col) col.enabled = false;

        foreach (var bird in GameObject.FindGameObjectsWithTag("Bird"))
        {
            Destroy(bird);
        }
        foreach (var diver in GameObject.FindGameObjectsWithTag("Skydiver"))
        {
            Destroy(diver);
        }
        foreach (var spawner in FindObjectsOfType<MonoBehaviour>())
        {
            if (spawner is birdSpawner || spawner is diverSpawn)
                spawner.enabled = false;
        }
        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private float TrySpawnExplosionAt(Vector3 pos)
    {
        if (explosionPrefab == null) return 0f;

        GameObject explode = Instantiate(explosionPrefab, pos, Quaternion.identity);
        ExplosionAnimation animation = explode.GetComponent<ExplosionAnimation>();
        if (animation != null && animation.frames != null && animation.frames.Length > 0 && animation.fps > 0f)
        {
            return animation.frames.Length / animation.fps;
        }
        return 0f;
    }
}
