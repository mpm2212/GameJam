using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; 

public class ExplosionAnimation : MonoBehaviour
{
    public Sprite[] frames;
    public float fps = 5;

    SpriteRenderer spr;
    int currentFrameIndex = 0;
    float frameTimer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        frameTimer = (1f / fps);
        currentFrameIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        frameTimer -= Time.deltaTime;

        if (frameTimer <= 0)
        {
            currentFrameIndex++;
            if (currentFrameIndex >= frames.Length)
            {
                Destroy(gameObject);
                return;
            }
            frameTimer = (1f / fps);
            spr.sprite = frames[currentFrameIndex];
        }
    }
}
