using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public Vector3 left = new Vector3(-12.0f, 0, 0);
    public Vector3 right = new Vector3(12.0f, 0, 0);
    public float movementSpeed = 0.008f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 transformPos = transform.position;
        if (transformPos.x > right.x)
        {
            transformPos.x = left.x;
            transform.position = transformPos;
        }
        transform.Translate(movementSpeed, 0f, 0f);
    }
}
