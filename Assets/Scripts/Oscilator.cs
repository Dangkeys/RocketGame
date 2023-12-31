using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscilator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    float movementFactor;
    [SerializeField] float period = 2f;
    private void Awake() {
        startingPosition = transform.position;
    }
    private void Update() {
        if(period <= Mathf.Epsilon) return;
        float cycles = Time.time / period;
        const float tau = Mathf.PI * 2;//const value of 6.283
        float rawSinWave = Mathf.Sin(cycles * tau);//going from -1 to 1
        movementFactor = (rawSinWave + 1f) / 2f;//change it into 0 to 1
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
