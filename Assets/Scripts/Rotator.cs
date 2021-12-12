using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attach the script to a GameObject to continuously rotate it in it's own axis.
/// </summary>
public class Rotator : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 50f;
    [SerializeField] bool oppositeDirection = false;

    void Start() 
    {
        // If the object has to rotate to the opposite direction,
        // set the rotation speed to negative
        rotationSpeed = oppositeDirection ? rotationSpeed * -1 : rotationSpeed;
    }

    void Update () 
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0, Space.World);
    }
}
