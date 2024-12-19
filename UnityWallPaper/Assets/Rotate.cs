using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Rotate : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(_angle, 0.0f, 0.0f);
    }
    private float _angle = 1.0f;
}
