using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private GameObject mainCam;

    private void Awake()
    {
        mainCam = GameObject.Find("Main Camera");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            mainCam.transform.SetParent(null);
            collision.gameObject.SetActive(false);
            this.gameObject.SetActive(false);
            Debug.Log("GAME OVER");
        }
    }

    private void TurnOffBullets()
    {

    }
}
