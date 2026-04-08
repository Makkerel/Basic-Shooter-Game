using UnityEngine;

public class enemy : MonoBehaviour
{
    private Transform player_transform;
    private float move_speed = 4f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player_transform = FindFirstObjectByType<player>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(player_transform != null) {
            transform.position = Vector3.MoveTowards(transform.position, player_transform.position, Time.deltaTime * move_speed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<player>() == null) {
            return;
        }

        other.gameObject.GetComponent<player>().Die();
    }
}
