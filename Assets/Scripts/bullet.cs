using UnityEngine;

public class bullet : MonoBehaviour
{
    private float speed = 20f;
    private player main_character;

    public AudioClip kill_sound;

    private void Awake()
    {
        main_character = FindFirstObjectByType<player>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * (speed * Time.deltaTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<enemy>() == null) {
            return;
        }
        AudioSource.PlayClipAtPoint(kill_sound, Camera.main.transform.position);
        main_character.updateScore(1);
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
