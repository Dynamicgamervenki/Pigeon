using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CurrentWire : MonoBehaviour
{
    public static CurrentWire instance;

    HealthSystem healthSystem;
   public  bool inContactWithCurrentPole = false;

    private void Awake()
    {
        healthSystem = FindObjectOfType<HealthSystem>();
    }


    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("LMAO");
            healthSystem.DecrementHealth(5);
             
        }
    }

}
