using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class repeatBack : MonoBehaviour
{
    Vector3 startPos;
    float repeatWidth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position;
        repeatWidth = GetComponent<BoxCollider2D>().size.x / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > startPos.x + repeatWidth)
        {
            transform.position = startPos;
        }
    }
}
