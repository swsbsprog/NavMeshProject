using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetFollow : MonoBehaviour
{
    public Vector3 offset;
    public Transform target;
    void Start()
    {
        offset = target.position
            - transform.position;
        // a = b - c
        // c = b - a
    }
    void Update()
    {
        transform.position = target.position
            - offset;
    }
}
