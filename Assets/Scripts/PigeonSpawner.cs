using UnityEngine;

public class PigeonSpawner : MonoBehaviour
{
    [SerializeField] public GameObject pigeonPrefab;
    [SerializeField] public float minY = -4.25f;
    [SerializeField] public float maxY = 4f;
    
    private float _currentTimer = 0f;
    public float spawnTimer = 2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _currentTimer += Time.deltaTime;
        if (_currentTimer >= spawnTimer)
        {
            SpawnNewBirds();
            _currentTimer = 0f;
        }
    }

    Vector3 SpawnNewBirds()
    {
        float randomY = Random.Range(minY, maxY);
        Vector3 spawnPosition = new Vector3(transform.position.x, randomY, 0f);
        Instantiate(pigeonPrefab, spawnPosition, Quaternion.identity);
        return spawnPosition;
    }
}
