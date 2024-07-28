using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.WebCam;

public class EnemieAi : MonoBehaviour
{
    private Animator anim;

    public float detectionRadius = 5.0f;
    public LayerMask playerMask;
    public bool PigeonWithinRadius = false;
    private GameObject pigeon;

    public float cooldownTime = 10.0f; // Time between shots
    private float nextShootTime = 0f; // Tracks when the enemy can shoot next

    public Transform shootPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 5.0f;

    public float rotationSpeed;

    public GameObject rightHand;
    private GameObject Ak47;
    private ParticleSystem shootingVfx;

    private void Awake()
    {
        pigeon = GameObject.Find("Player");
        anim = GetComponent<Animator>();
        Ak47 = this.gameObject.transform.GetChild(9).gameObject;
        shootingVfx = Ak47.transform.GetChild(0).GetComponent<ParticleSystem>();  
    }

    private void Update()
    {
        DetectAndShootPigeon();
       if(PigeonWithinRadius)
            LookAndRotateTowardsPigeon();

       if(!PigeonWithinRadius && pigeon.gameObject.activeInHierarchy)
            anim.SetBool("isShooting", false);

        if (!pigeon.gameObject.activeInHierarchy)
            anim.SetBool("dance", true);

    }

    private void DetectAndShootPigeon()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius, playerMask);
        PigeonWithinRadius = hitColliders.Length > 0;

        if (PigeonWithinRadius && Time.time >= nextShootTime)
        {
            Debug.Log("Shooting the pigeon");
            ShootPigeon();
            nextShootTime = Time.time + cooldownTime;
        }
    }

    private void ShootPigeon()
    {
        anim.SetBool("isShooting", true);
        AttachGun();
        shootingVfx.Play();
        GameObject instantiatedBullet =  Instantiate(bulletPrefab, shootPoint.position,Quaternion.identity);
        Vector3 directionToPlayer = (pigeon.transform.position - shootPoint.position).normalized;
        Rigidbody bulletRigidbody = instantiatedBullet.GetComponent<Rigidbody>();
        if (bulletRigidbody != null)
        {
            bulletRigidbody.velocity = directionToPlayer * bulletSpeed;
            AudioManager.Instance.PlaySFX("Shooting", 0.75f);
        }

    }

    public void AttachGun()
    {
        Ak47.gameObject.SetActive(true);
        Ak47.gameObject.transform.SetParent(rightHand.transform, true);
        Ak47.gameObject.transform.localPosition = new Vector3(-0.0286826193f, 0.201789796f, 0.0566200167f);
        Ak47.gameObject.transform.localRotation = Quaternion.Euler(334.487518f, 197.074829f, 266.034729f);
    }







    private void LookAndRotateTowardsPigeon()
    {
        Vector3 directionToPlayer = pigeon.transform.position - transform.position;
        directionToPlayer.y = 0; // Keep only the horizontal direction

        if (directionToPlayer != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = PigeonWithinRadius ? Color.red : Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
