using UnityEngine;

public class birdSpawner : MonoBehaviour
{
   [Tooltip("A prefab that is instantiated when the skydiver is destroyed")]
   public float spawnWidth = 1;
   [Tooltip("How many Skydivers spawn per second?")]
   public float spawnRate = 1;
   [Tooltip("The prefab that is to be instantiated as a skydiver")]
   public GameObject birdPrefab;


   private float lastSpawnTime = 0;
   [SerializeField] public float minY = -4.25f;
   [SerializeField] public float maxY = 4.0f;


   /// <summary>
   /// Update is called by Unity. This will spawn asteroids while the game is in play mode.
   /// </summary>
   void Update() {
       // this is a simple timer structure that executes every 1/spawnRate seconds. This means it spawns spawnRate asteroids every second.
       if (lastSpawnTime + 1 / spawnRate < Time.time) {
           lastSpawnTime = Time.time;
           Vector3 spawnPosition = transform.position;
           float randY = Random.Range(minY, maxY);
           spawnPosition += new Vector3(Random.Range(-spawnWidth, spawnWidth), randY, 0);
           // the Instatiate function creates a new GameObject copy (clone) from a Prefab at a specific location and orientation.
           Instantiate(birdPrefab, spawnPosition, Quaternion.identity);
       }
   }


   /// <summary>
   /// Helper function called by unity to draw gizmos for debugging and orientation in the scene view. Is not part of any game logic.
   /// </summary>
   void OnDrawGizmos(){
       Gizmos.DrawLine (transform.position - new Vector3 (spawnWidth, 0, 0), transform.position + new Vector3 (spawnWidth, 0, 0));
       Gizmos.DrawLine (transform.position - new Vector3 (spawnWidth, 1, 0), transform.position - new Vector3 (spawnWidth, -1, 0));
       Gizmos.DrawLine (transform.position + new Vector3 (spawnWidth, 1, 0), transform.position + new Vector3 (spawnWidth, -1, 0));
   }
}
