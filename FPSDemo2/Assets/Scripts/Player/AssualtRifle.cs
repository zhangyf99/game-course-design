using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssualtRifle : Gun
{
    private IEnumerator ReloadCheckCoroutine;
    private IEnumerator doAnimCoroutine;
    private fps_Camera fps_Camera;

    protected override void Start()
    {
        base.Start();
        doAnimCoroutine = DoAnim();
        fps_Camera = FindObjectOfType<fps_Camera>(); 
    }

    private void Update()
    {
        if (Input.GetButton("Fire1") && currentAmmo > 0 && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            CreateBullet();
            Shoot();
            //gunFlash.Play(0);
       }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }

                //Aim
        if(Input.GetMouseButtonDown(1))
        {
            isAiming = true;
            Aim();
        }

        //Exit Aim 
        if (Input.GetMouseButtonUp(1))
        {
            isAiming = false;
            Aim(); 
        }
    }

    protected override void Reload()
    {
        gunAnimator.SetLayerWeight(2, 1);
        gunAnimator.SetTrigger(currentAmmo > 0 ? "ReloadLeft":"ReloadOutOf");

        reloadAudio.clip = currentAmmo > 0 ? reloadLeftClip : reloadOutOfClip;
        reloadAudio.Play();

        //CheckReloadAnimationEnd();
        //StartCoroutine(CheckReloadAnimationEnd());
        currentAmmo = AmmoInMag;
        currentMaxAmmoCarried -= AmmoInMag;
        //Debug.Log("Reload");
    }

    protected override void Shoot()
    {
        currentAmmo -= 1;
        MuzzleParticle.Play();
        //gunAnimator.Play("Fire");
        gunAnimator.Play("Fire",isAiming?1:0);

        shootAudio.clip = shootClip;
        shootAudio.Play();

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out shootHit, range, shootableMask))
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
        CasingParticle.Play();
        fps_Camera.FireTest(); 
    }

    protected override void CreateBullet()
    {
        GameObject tempBullet = Instantiate(BulletPrefab, MuzzlePoint.position, MuzzlePoint.rotation);
        var tempBulletRb = tempBullet.AddComponent<Rigidbody>();
        tempBulletRb.velocity = tempBulletRb.transform.forward * 100f;
    }

    private void CheckReloadAnimationEnd()
    {
        gunStateInfo = gunAnimator.GetCurrentAnimatorStateInfo(2);
        Debug.Log(gunStateInfo);
        Debug.Log(gunStateInfo.IsTag("Reload"));
        if (gunStateInfo.IsTag("Reload"))
        {
            Debug.Log(gunStateInfo.normalizedTime);
            if (gunStateInfo.normalizedTime >= 0.9f)
            {
                currentAmmo = AmmoInMag;
                currentMaxAmmoCarried -= AmmoInMag;
                return;
            }
        }
    }

    protected override void Aim()
    {
        gunAnimator.SetBool("Aim", isAiming);
        if(doAnimCoroutine == null)
        {
            doAnimCoroutine = DoAnim();
            StartCoroutine(doAnimCoroutine);
        }
        else
        {
            StopCoroutine(doAnimCoroutine);
            doAnimCoroutine = null;
            doAnimCoroutine = DoAnim();
            StartCoroutine(doAnimCoroutine);
        }

    }

    /*IEnumerator CheckReloadAnimationEnd()
    {
        //Debug.Log(GunAnimator.GetCurrentAnimatorStateInfo(2).normalizedTime);
        //yield return null;
        GunStateInfo = GunAnimator.GetCurrentAnimatorStateInfo(2);
        if (GunStateInfo.IsTag("ReloadAmmo"))
        {
            Debug.Log(GunStateInfo.normalizedTime);
            if(GunStateInfo.normalizedTime>=0.9f)
            {
                CurrentAmmo = AmmoInMag;
                CurrentMaxAmmoCarried -= AmmoInMag;
                yield break;
            }
        }
    }*/

    IEnumerator DoAnim()
    {
        while(true)
        {
            yield return null;

            float currentFOV = 0;
            fpsCam.fieldOfView = Mathf.SmoothDamp(fpsCam.fieldOfView, isAiming? 20:orginFOV, ref currentFOV, Time.deltaTime * 2);
        }
        
    }


}
