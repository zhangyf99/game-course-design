using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public Image damageImage;
    //the sound 
    public AudioClip deathClip;
    //how quickly the damage image flashes up 
    public float flashSpeed = 5f;
    //red
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

    public AudioSource playerAudio;
    AudioClip hurtClip;
    //a reference to another scr ipt
    fps_PlayerManager playerMovement;
    Gun playerShooting;
    bool isDead;
    bool damaged;
    public bool cannotHurt = false;

    private BinaryFormatter bf = new BinaryFormatter();   // 二进制格式化程序
    private FileStream fileStream;

    void Awake ()
    {
        //var audioArray = GetComponents(typeof(AudioSource));
        //playerAudio = (AudioSource)audioArray[1];
        playerMovement = GetComponent <fps_PlayerManager> ();
        //the script is on the child object GunBarrelEnd
        playerShooting = GetComponentInChildren <Gun> ();
        if (PlayerPrefs.GetInt("Load", 0) == 1 && File.Exists(Application.dataPath + "/StreamingAssets/bin" +
                PlayerPrefs.GetInt("Level", 0) + ".txt"))
        {
            fileStream = File.Open(Application.dataPath + "/StreamingAssets/bin" +
                PlayerPrefs.GetInt("Level", 0) + ".txt", FileMode.Open);
            Save readSave = (Save)bf.Deserialize(fileStream);
            fileStream.Close();
            currentHealth = readSave.playerHealth;
            gameObject.transform.position = new Vector3(readSave.playerPosition[0],
                readSave.playerPosition[1], readSave.playerPosition[2]);
        }
        else
        {
            currentHealth = startingHealth;
        }      
    }


    void Update ()
    {
        if(damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
    }

    //other components can call this function
    //enemy calls the function 
    public void TakeDamage (int amount)
    {
        if(cannotHurt)
        {
            return;
        }

        damaged = true;

        currentHealth -= amount;

        healthSlider.value = currentHealth;

        playerAudio.Play ();

        if(currentHealth <= 0 && !isDead)
        {
            Death ();
        }
    }


    void Death ()
    {
        isDead = true;

        //playerShooting.DisableEffects ();

        playerAudio.clip = deathClip;
        playerAudio.Play ();

        playerMovement.enabled = false;
        playerShooting.enabled = false;
    }


    public void RestartLevel ()
    {
        SceneManager.LoadScene (0);
    }
}
