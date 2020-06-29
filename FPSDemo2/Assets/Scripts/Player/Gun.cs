using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public abstract class Gun : MonoBehaviour
{
    //damege per shot
    public int damagePerShot = 20;
    //the range of bullet
    public float range = 100f;
    //the most num to fire in one second
    public float fireRate = 15f;
    protected int shootableMask;
    protected RaycastHit shootHit;

    public Camera fpsCam;
    public AudioSource shootAudio;
    public AudioSource reloadAudio;
    public AudioClip shootClip;
    public AudioClip reloadLeftClip;
    public AudioClip reloadOutOfClip;
    public Transform MuzzlePoint;
    public Transform CasingPoint;

    public ParticleSystem MuzzleParticle;
    public ParticleSystem CasingParticle;

    //ammo num in a mag
    public int AmmoInMag = 30;
    //the sum num of ammo
    public int MaxAmmoCarried = 120;
    public GameObject BulletPrefab;

    protected int currentAmmo;
    protected int currentMaxAmmoCarried;
    protected float nextTimeToFire;
    protected Animator gunAnimator;
    protected AnimatorStateInfo gunStateInfo;
    protected float orginFOV;
    protected bool isAiming=false;

    private BinaryFormatter bf = new BinaryFormatter();   // 二进制格式化程序
    private FileStream fileStream;

    void Awake()
    {
        //find the enemy layer befored by "Shootable"
        //shootableMask = LayerMask.GetMask("Shootable");
        //gunAudio = GetComponent<AudioSource>();
    }

    protected virtual void Start() {
        if (PlayerPrefs.GetInt("Load", 0) == 1 && File.Exists(Application.dataPath + "/StreamingAssets/bin" +
               PlayerPrefs.GetInt("Level", 0) + ".txt"))
        {
            fileStream = File.Open(Application.dataPath + "/StreamingAssets/bin" +
                PlayerPrefs.GetInt("Level", 0) + ".txt", FileMode.Open);
            Save readSave = (Save)bf.Deserialize(fileStream);
            fileStream.Close();
            currentAmmo = readSave.ammoNum;
            currentMaxAmmoCarried = readSave.maxAmmoNum;
        }
        else
        {
            currentAmmo = AmmoInMag;
            currentMaxAmmoCarried = MaxAmmoCarried;
        }

        shootableMask = LayerMask.GetMask("Shootable");
        shootAudio = GetComponent<AudioSource>();
        gunAnimator = GetComponent<Animator>();
        orginFOV = fpsCam.fieldOfView;
    }

  

    protected abstract void Shoot();

    protected abstract void Reload();

    protected abstract void CreateBullet();

    protected abstract void Aim();

    public int GetCurrentAmmo()
    {
        return currentAmmo;
    }

    public int GetCurrentMaxAmmo()
    {
        return currentMaxAmmoCarried;
    }
}
