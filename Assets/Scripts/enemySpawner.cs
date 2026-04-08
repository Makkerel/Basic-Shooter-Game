using UnityEngine;

public class enemySpawner : MonoBehaviour
{
    public GameObject enemy_prefab;
    private float spawn_interval = 0.5f;
    private float spawn_time = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= spawn_time) {
            spawn_time = Time.time + spawn_interval;

            Vector2 spawn_location = 13 * Random.insideUnitCircle.normalized;
            Instantiate(enemy_prefab, new Vector3(spawn_location.x, 1.5f, spawn_location.y), Quaternion.identity);
        }
    }
}
