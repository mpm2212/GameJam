using UnityEngine;
using TMPro;

public class DistanceText : MonoBehaviour
{
    public Transform target; 
    public TextMeshProUGUI distanceText; 
    public float xDistance;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        xDistance += Time.deltaTime; 
        distanceText.text = "Distance: " + xDistance.ToString("F2");
    }
}
