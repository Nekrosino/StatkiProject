using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private BezierTest bezierScript;

    private void Start()
    {
        bezierScript = BezierTest.instance;
    }

    private void Update()
    {
        float currentWaveHeight = bezierScript.GetWaveHeightAtPosition(transform.position);
        Vector3 newPosition = transform.position;
        newPosition.y = currentWaveHeight;
        transform.position = newPosition;

        // Tutaj mo¿esz dodaæ logikê ruchu statku w osiach X i Z
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput);
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        currentWaveHeight = bezierScript.GetWaveHeightAtPosition(transform.position);
        Debug.Log("Current Wave Height at Ship Position: " + currentWaveHeight);

    }
}