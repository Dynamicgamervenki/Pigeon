using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private GameObject mainCam;
    PlayerController playerController;

    private void Awake()
    {
        mainCam = GameObject.Find("Main Camera");
        playerController = FindObjectOfType<PlayerController>();
        StartCoroutine(TurnOffBullets());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            playerController.blood.Play();
            mainCam.transform.SetParent(null);
            collision.gameObject.SetActive(false);
            this.gameObject.SetActive(false);
            Debug.Log("GAME OVER");
        }

        if(collision.gameObject.tag == "Building")
        {
            this.gameObject.SetActive(false);
        }

    }


    private IEnumerator TurnOffBullets()
    {
        yield return new WaitForSeconds(5.0f);
        this.gameObject.SetActive(false);

    }
}
