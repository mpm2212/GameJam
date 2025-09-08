using UnityEngine;

public class BirdAnimation : MonoBehaviour
{
    [SerializeField] public Sprite[] birdSprites;
    [SerializeField] private float animationFPS = 5f;

    private Rigidbody2D rb2d;
    private SpriteRenderer spriteR;
    private float frameTimer = 0.0f;
    private int frameIndex = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteR = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        updateBirdSprite();
    }

    void updateBirdSprite()
    {
        frameTimer -= Time.deltaTime; //countdown timer
        if (frameTimer <= 0) //time to change frame
        {
            if (animationFPS <= 0)
            {
                Debug.Log("FPS is less than 0");
                animationFPS = 5f; } 

            frameTimer = 1f / animationFPS; //reset timer
            spriteR.flipX = true;
            frameIndex %= birdSprites.Length; 
            
            
            spriteR.sprite = birdSprites[frameIndex];
            frameIndex++;
        }
    }
}