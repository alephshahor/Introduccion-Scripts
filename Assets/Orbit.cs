using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Orbit : MonoBehaviour
{
    public Vector3 transformation;
    public float velocity;
    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        transform.RotateAround(initialPosition + transformation, Vector3.up, velocity * Time.deltaTime);
    }
}




