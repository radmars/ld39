using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    float lastSpawnTime;
    int critterMask;
    public int spawnRadius = 30;

    void Start()
    {
        critterMask = LayerMask.GetMask("Critters");
    }

    bool ShouldSpawn(float lastSpawnTime, float now)
    {
        return (now - lastSpawnTime > 1)
            && (Random.Range(0, 10) > 1);
    }

    void FixedUpdate()
    {
        if (ShouldSpawn(lastSpawnTime, Time.time))
        {
            var near = GetRandomInactiveCritter(spawnRadius);
            if (near)
            {
                lastSpawnTime = Time.time;
                SpawnACritter(near);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }

    private Collider GetRandomInactiveCritter(int radius)
    {
        var nearbyCritters = Physics.SphereCastAll(transform.position, radius, Vector3.up, Mathf.Infinity, critterMask);
        nearbyCritters = nearbyCritters.Where((hit) => !hit.collider.GetComponent<MeshRenderer>().enabled).ToArray();
        if (nearbyCritters.Length > 0)
        {
            return nearbyCritters[Random.Range(0, nearbyCritters.Length)].collider;
        }
        return null;

    }

    void SpawnACritter(Collider c)
    {
        c.gameObject.SendMessage("ActivateCritter");
    }
}
