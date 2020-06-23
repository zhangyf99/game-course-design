using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public PlayerHealth playerHealth;

    public TimeManager timeManager;

    Animator anim;


    void Awake()
    {
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        if (playerHealth.currentHealth <= 0 || timeManager.countDown==0)
        {
            anim.SetTrigger("GameOver");
        }
    }
}
