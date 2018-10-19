using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraLifePickup : MonoBehaviour {
    public float amplitude = 0.5f;
    public float period = 2f;

    Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    protected void Update()
    {
        float theta = Time.timeSinceLevelLoad / period;
        float distance = amplitude * Mathf.Sin(theta);
        transform.position = startPos + Vector3.up * distance;
    }
}
