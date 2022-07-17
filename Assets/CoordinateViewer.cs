using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CoordinateViewer : MonoBehaviour
{
    public Vector3 forward;
    public Vector3 right;
    public Vector3 up;

    public bool drawForward;
    public bool drawRight;
    public bool drawUp;
    void Update()
    {
        forward = transform.forward;
        right = transform.right;
        up = transform.up;

        if (drawForward)
            Debug.DrawLine(transform.position, transform.position + forward, new Color(1, 0, 0));
        if (drawRight)
            Debug.DrawLine(transform.position, transform.position + right, new Color(0, 1, 0));
        if (drawUp)
            Debug.DrawLine(transform.position, transform.position + up, new Color(0, 0, 1));
    }
}
