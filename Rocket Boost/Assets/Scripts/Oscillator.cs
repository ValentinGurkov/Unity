using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour {
    [SerializeField] private Vector3 movementVector = new Vector3(10f, 10f, 10f);
    [SerializeField] private float period = 2f;

    private const float tau = Mathf.PI * 2f;
    private float cycles;
    private float rawSineWave;
    private float movementFactor;
    private Vector3 offset;
    private Vector3 startingPos;

    // Start is called before the first frame update
    // Start is called before the first frame update
    private void Start() {
        startingPos = transform.position;
    }

    // Update is called once per frame
    private void Update() {
        if (period <= Mathf.Epsilon) {
            return;
        }
        cycles = Time.time / period;
        rawSineWave = Mathf.Sin(cycles * tau);
        movementFactor = rawSineWave / 2f + 0.5f;
        offset = movementVector * movementFactor;
        transform.position = startingPos + offset;
    }
}