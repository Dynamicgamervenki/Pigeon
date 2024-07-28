using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Poop : MonoBehaviour
{
    private ParticleSystem poopVfx;

    public Text score;

    private void Awake()
    {
        poopVfx = this.gameObject.transform.GetChild(0).GetComponent<ParticleSystem>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object is tagged as "Player"
        if (collision.gameObject.CompareTag("Player"))
        {
            return; // Do nothing if it collides with the player
        }

        if (collision.gameObject.CompareTag("Enemie"))
        {
            ScoreManager.instance.IncreaseScore(1);
            StartCoroutine(PoopFalse(collision.gameObject));
        }
        else
        {
            StartCoroutine(PoopFalse(this.gameObject));
        }

    }

    IEnumerator PoopFalse(GameObject gg)
    {
        poopVfx.Play();
        yield return new WaitForSeconds(0.10f);
        gg.SetActive(false);
    }
}
