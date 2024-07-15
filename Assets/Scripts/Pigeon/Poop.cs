using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poop : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Enemie")
            collision.gameObject.SetActive(false);
        else
            gameObject.SetActive(false);
    }


}
