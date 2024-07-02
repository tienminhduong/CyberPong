using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float minSpeed;
    [SerializeField] private GameObject floatingText;

    Rigidbody rb;

    private float speed;
    private Vector3 direction;
    private bool started;

    private readonly Vector3 startDirection = new Vector3(0.5f, 0f, 0.5f);
    private Vector3 startPosition;

    private void Awake() {
        startPosition = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        ResetBall();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !started) {
            StartBall();
        }
    }

    void FixedUpdate() {
        rb.MovePosition(transform.position + speed * Time.deltaTime * direction);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Wall")) {
            speed += 0.5f;
            direction.z = -direction.z;
        }
        else if (other.CompareTag("Racket")) {
            speed += 1f;
            //direction.x = -direction.x;
            Vector3 newDirection = (transform.position - other.transform.position).normalized;
            newDirection.x = Mathf.Sign(newDirection.x) * Mathf.Max(0.5f, newDirection.x);
            newDirection.z = Mathf.Sign(newDirection.z) * Mathf.Max(0.5f, newDirection.z);

            direction = newDirection;
        }
    }

    public void ResetBall() {
        transform.position = startPosition;
        direction = Vector3.zero;
        speed = minSpeed;
        GameManager.CurrentState = GameState.Paused;
        floatingText.SetActive(true);
        started = false;
    }

    private void StartBall() {
        direction = startDirection;
        direction.x *= Mathf.Sign(Random.Range(-1f, 1f));
        direction.z *= Mathf.Sign(Random.Range(-1f, 1f));
        GameManager.CurrentState = GameState.Playing;
        floatingText.SetActive(false);
        started = true;
    }
}
