using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poop : MonoBehaviour
{
    private ParticleSystem poopVfx;

    private void Awake()
    {
        poopVfx = this.gameObject.transform.GetChild(0).GetComponent<ParticleSystem>();
    }


    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Enemie")
        {      
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
