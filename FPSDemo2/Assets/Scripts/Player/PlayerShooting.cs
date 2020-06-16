using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 20;
    //the min dist between bullets
    public float timeBetweenBullets = 0.15f;
    //the range of bullet
    public float range = 100f;


    float timer;
    Ray shootRay;
    RaycastHit shootHit;
    int shootableMask;
    ParticleSystem gunParticles;
    LineRenderer gunLine;
    AudioSource gunAudio;
    Light gunLight;
    float effectsDisplayTime = 0.2f;


    void Awake ()
    {
        //find the enemy layer befored by "Shootable"
        shootableMask = LayerMask.GetMask ("Shootable");
        gunParticles = GetComponent<ParticleSystem> ();
        gunLine = GetComponent <LineRenderer> ();
        gunAudio = GetComponent<AudioSource> ();
        gunLight = GetComponent<Light> ();
    }


    void Update ()
    {
        timer += Time.deltaTime;

		if(Input.GetButton ("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0)
        {
            Shoot ();
        }

        if(timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects ();
        }
    }


    //enable the effect of line and light
    public void DisableEffects ()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }


    void Shoot ()
    {
        //reset
        timer = 0f;

        gunAudio.Play ();

        gunLight.enabled = true;

        //reset and start the special effect
        gunParticles.Stop ();
        gunParticles.Play ();

        gunLine.enabled = true;
        gunLine.SetPosition (0, transform.position);

        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        //para:orgin&direction,out info,mixdist,int laymask
        if(Physics.Raycast (shootRay, out shootHit, range, shootableMask))
        {
            EnemyHealth enemyHealth = shootHit.collider.GetComponent <EnemyHealth> ();
            if(enemyHealth != null)
            {
                //para:damege amount,collision point(special effect to be added)
                enemyHealth.TakeDamage (damagePerShot, shootHit.point);
            }
            //para_2:the tail of gunline:collision point/shoot point,1 correspond to the tail of the gun
            gunLine.SetPosition (1, shootHit.point);
        }
        else
        {
            //set the tail of gunline to be the current direction with range
            gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
        }
    }
}
