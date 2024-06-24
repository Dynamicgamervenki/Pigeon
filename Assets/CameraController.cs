using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Camera Settings")]
    public Transform followTarget;
    public float distance;
    public float minVerticalAngel = -20.0f;
    public float maxVerticalAngel = 45.0f;
    public Vector2 frameOffset;
    public float rotationSpeed = 2.0f;
    public bool InvertX;
    public bool InvertY;

    public float invertXvalue;
    public float invertYvalue;

    [Header("Camera Inputs")]
    public float rotationX;
    public float rotationY;

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        invertXvalue = (InvertX) ? -1 : 1;
        invertYvalue = (InvertY) ? -1 : 1;

        rotationX += Input.GetAxis("Camera Y") * invertYvalue * rotationSpeed;
        rotationX = Mathf.Clamp(rotationX, minVerticalAngel, maxVerticalAngel);

        rotationY += Input.GetAxis("Camera X") * invertXvalue * rotationSpeed;

        var targetRotation = Quaternion.Euler(rotationX, rotationY, 0f);
        var focousPosition = followTarget.position + new Vector3(frameOffset.x, frameOffset.y);

        transform.position = focousPosition - targetRotation * new Vector3(0, 0, distance);
        transform.rotation = targetRotation;

    }

    public Quaternion PlanerRotation => Quaternion.Euler(0f, rotationY, 0f);                                                //c# properties concept.



}
