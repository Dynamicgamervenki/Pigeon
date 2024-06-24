using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraManager : MonoBehaviour
{
    public float rotationY;
    public float rotationX;

    public float rotationSpeed = 100.0f;
    public float minHorizontalValue = -60.0f;
    public float maxHorizontalValue = 60.0f;

    private void Update()
    {
        //rotationY += Input.GetAxis("Mouse X") * rotationSpeed;
        //rotationY = Mathf.Clamp(rotationY, minHorizontalValue, maxHorizontalValue);
        //var targetRotation = Quaternion.Euler(0f, rotationY, 0f);
        //transform.rotation = targetRotation;
    }
}
