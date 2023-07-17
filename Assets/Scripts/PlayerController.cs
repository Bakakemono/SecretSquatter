using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float horizontalInput = 0;
    float speed = 10.0f;

    private void Update() {
        horizontalInput = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate() {
        transform.position = transform.position + new Vector3(horizontalInput, 0, 0) * speed * Time.fixedDeltaTime;
    }
}
