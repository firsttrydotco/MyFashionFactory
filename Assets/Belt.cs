using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Belt : MonoBehaviour
{
    public Rigidbody rb;
    public float speed;
    public Vector3 direction;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        Vector3 pos = rb.position;
        rb.position += direction * speed * Time.fixedDeltaTime;
        rb.MovePosition(pos);
    }
}