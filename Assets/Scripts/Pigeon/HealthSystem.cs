using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public Slider slider;
    PlayerController playerController;
    private GameObject mainCam;

    private float fallSpeed = 0.0f;
    private float gravity = -150.0f;

    public float decreaseRate = 1f;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        mainCam = GameObject.Find("Main Camera");
        playerController = FindObjectOfType<PlayerController>();
        InitalHealth();
    }

    private void Update()
    {
        CheckingHealthSytsem();
    }

    private void InitalHealth()
    {
        slider.value = slider.maxValue;
    }
    
    public void IncrementHealth(float amount)
    {
        slider.value += amount;
    }

    public void DecrementHealth(float amount)
    {
        slider.value -= amount;
    }

    private void CheckingHealthSytsem()
    {
        if(slider.value <= 1.0f)
        {
            fallSpeed += gravity * Time.deltaTime;

            // Move the pigeon downwards
            Vector3 move = new Vector3(0, fallSpeed, 0);
            mainCam.transform.SetParent(null);
            playerController.GetComponent<CharacterController>().Move(move * Time.deltaTime);
            Invoke("DelayMin", 0.5f);
        }
    }

    private void DelayMin()
    {
        playerController.gameObject.SetActive(false);
    }
}
