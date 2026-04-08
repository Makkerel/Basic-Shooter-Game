using UnityEngine;
using UnityEngine.InputSystem;

public class player : MonoBehaviour
{
    private int score = 0;
    public TMPro.TMP_Text score_text;
    public GameObject game_over_text;

    // movement controls
    private float move_speed = 10f;
    private InputAction move_action;
    private InputAction fire_action;

    // for mouse tracking
    private Camera mainCamera;
    private Plane plane;

    //shooting
    public GameObject bullet_prefab;
    private float fire_rate = 0.1f;
    private float fire_time = 0f;

    //sound
    public AudioClip shoot_sound;
    private AudioSource shoot_source;

    private void Awake()
    {
        shoot_source = GetComponent<AudioSource>();
        game_over_text.SetActive(false);
        mainCamera = Camera.main;
        plane = new Plane(Vector3.up, Vector3.zero);

        move_action = new InputAction("Move", InputActionType.Value);

        move_action.AddCompositeBinding("2DVector")
            .With("Up", "<Keyboard>/w")
            .With("Down", "<Keyboard>/s")
            .With("Left", "<Keyboard>/a")
            .With("Right", "<Keyboard>/d");

        fire_action = new InputAction("Fire", InputActionType.Button);
        fire_action.AddBinding("<Mouse>/leftButton");
    }

    private void OnEnable()
    {
        move_action.Enable();
        fire_action.Enable();
    }

    private void OnDisable()
    {
        move_action.Disable();
        fire_action.Disable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Aim();
        Shoot();
    }
    private void Movement()
    {
        Vector2 input_move = move_action.ReadValue<Vector2>();
        Vector3 direction = new Vector3(input_move.x, 0f, input_move.y);

        transform.position += direction * move_speed * Time.deltaTime;
    }

    private void Aim()
    {
        Vector2 mouseScreenPosition = Mouse.current.position.ReadValue();
        Ray ray = mainCamera.ScreenPointToRay(mouseScreenPosition);

        if (plane.Raycast(ray, out float enterDistance)) {
            Vector3 hitPoint = ray.GetPoint(enterDistance);
            Vector3 targetPoint = new Vector3(hitPoint.x, transform.position.y, hitPoint.z);
            transform.LookAt(targetPoint);
        }
    }
    private void Shoot()
    {
        if(fire_action.IsPressed() && Time.time >= fire_time) {
            fire_time = Time.time + fire_rate;
            shoot_source.PlayOneShot(shoot_sound);
            Instantiate(bullet_prefab, transform.position, transform.rotation);
        }
    }

    public void Die()
    {
        game_over_text.SetActive(true);
        Time.timeScale = 0f;
        Destroy(gameObject);
    }
    
    public void updateScore(int value)
    {
        score += value;
        score_text.text = $"Score: {score}";
    }
}
