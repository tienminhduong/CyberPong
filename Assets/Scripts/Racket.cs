using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Racket : MonoBehaviour
{
    [SerializeField] private KeyCode up, down;
    [SerializeField] private float speed = 10f;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isPressedUp = Input.GetKey(up);
        bool isPressedDown = Input.GetKey(down);
        if (isPressedUp) { rb.velocity = Vector3.forward * speed; }
        else if (isPressedDown) { rb.velocity = Vector3.back * speed; }
        else { rb.velocity = Vector3.zero; }
    }
}
