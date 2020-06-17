using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public int damagePerShot = 20;
    //the range of bullet
    public float range = 100f;
    //the most num to fire in one second
    public float fireRate = 15f;

    public Camera fpsCam;
    int shootableMask;
    RaycastHit shootHit;
    public ParticleSystem gunFlash;
    AudioSource gunAudio;

    private float nextTimeToFire;

    void Awake()
    {
        //find the enemy layer befored by "Shootable"
        shootableMask = LayerMask.GetMask("Shootable");
        gunAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
            gunFlash.Play();
        }
    }

    void Shoot()
    {
        gunAudio.Play();

        if (Physics.Raycast(fpsCam.transform.position,fpsCam.transform.forward,out shootHit, range,shootableMask))
        {
            EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                //para:damege amount,collision point(special effect to be added)
                enemyHealth.TakeDamage(damagePerShot, shootHit.point);
            }
            //para_2:the tail of gunline:collision point/shoot point,1 correspond to the tail of the gun
            //gunLine.SetPosition(1, shootHit.point);
        }

    }


}
