using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class currentWire : MonoBehaviour
{
    public GameObject pigeon;

    private void Awake()
    {
        pigeon = pigeon = GameObject.Find("Player");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("current");
        }
    }
}
