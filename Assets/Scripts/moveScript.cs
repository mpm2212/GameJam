using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class moveScript : MonoBehaviour
{
    public float speed = 6f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * speed);
    }
}
