using UnityEngine;

public class Spawner : MonoBehaviour
{
    Mover rover;
    float lastSpawnTime;
    public Critter[] critterPrefabs;

    void Start()
    {
        rover = FindObjectOfType<Mover>();
    }

    bool ShouldSpawn(float lastSpawnTime, float now)
    {
        return (now - lastSpawnTime > 5)
            && (Random.Range(0, 10) > 1);
    }

    void FixedUpdate()
    {
        if (ShouldSpawn(lastSpawnTime, Time.time))
        {
        Debug.Log("Check check?");
            lastSpawnTime = Time.time;
            SpawnACritter();
        }
    }

    void SpawnACritter()
    {
        // Pick a random place around the rover
        var around = Random.insideUnitCircle.normalized;
        // Offset from the player
        var offset = rover.transform.position + new Vector3(around.x, around.y);
        // Scale to a range away from the player 
        offset *= Random.Range(20, 40);

        // Scan for terrain
        var heightTestRay = new Ray(
            new Vector3(offset.x, offset.y) + new Vector3(0, 100),
            Vector3.down
        );

        RaycastHit collision;
        if (Physics.Raycast(heightTestRay, out collision, float.MaxValue, LayerMask.GetMask("Terrain")))
        {
            var prefab = critterPrefabs[Random.Range(0, critterPrefabs.Length)];
            var instance = Instantiate(prefab);
            instance.transform.position = collision.point;
        }
    }
}
