using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    Vector3 previousPosition = Vector3.zero;
    Vector3 bestPosition = Vector3.zero;

    float nextPosTimePrevision = 2.0f;
    float lerpSpeed = 5.0f;

    float maxDist = 1.0f;


    private void Awake() {
        previousPosition = playerTransform.position;
        bestPosition = playerTransform.position;
    }

    private void FixedUpdate() {
        bestPosition =
            playerTransform.position +
            Vector3.ClampMagnitude((playerTransform.position - previousPosition) / Time.fixedDeltaTime * nextPosTimePrevision, maxDist);

        transform.position = Vector2.Lerp(transform.position, bestPosition, lerpSpeed * Time.fixedDeltaTime);

        transform.position = new Vector3(transform.position.x, transform.position.y, -10);

        previousPosition = playerTransform.position;
    }
    public void ChangingFloor() {
        transform.position = playerTransform.position + new Vector3(0, 0, -10);
        previousPosition = playerTransform.position;
        bestPosition = playerTransform.position;
    }
}
