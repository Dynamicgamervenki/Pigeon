using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private GameObject mainCam;
    PlayerController playerController;
    public HealthSystem healthSystem;

    private void Awake()
    {
        mainCam = GameObject.Find("Main Camera");
        playerController = FindObjectOfType<PlayerController>();
        healthSystem = FindObjectOfType<HealthSystem>();
        StartCoroutine(TurnOffBullets());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            playerController.blood.Play();
            this.gameObject.SetActive(false);
            healthSystem.DecrementHealth(33.3f);
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
