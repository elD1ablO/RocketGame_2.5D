using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    Vector3 startPosition;
    [SerializeField] Vector3 movementVector;
    [SerializeField] [Range(0, 1)] float movementFactor;
    [SerializeField] float period = 2f;
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float cycles = Time.time / period;    // gowing over time
        
        const float tau = Mathf.PI * 2;       // constant of 6.283
        float rawSinWave = Mathf.Sin(cycles * tau);  //going from -1 to 1

        movementFactor = (rawSinWave + 1f) / 2f;    //recalculated from 0 to 1 s oits cleaner

        Vector3 offset = movementVector * movementFactor;
        transform.position = startPosition + offset;
    }
}
