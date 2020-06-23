using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public PlayerHealth playerHealth;

    public TimeManager timeManager;

    Animator anim;

    private BinaryFormatter bf = new BinaryFormatter();   // 二进制格式化程序
    private FileStream fileStream;


    void Awake()
    {
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        if (playerHealth.currentHealth <= 0 || timeManager.countDown==0)
        {
            anim.SetTrigger("GameOver");
            GameObject.FindGameObjectWithTag("TimeController").GetComponent<TimeManager>().timeFlying = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().cannotHurt = true;
            Invoke("load", 5.0f);
        }
    }

    private void load()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
