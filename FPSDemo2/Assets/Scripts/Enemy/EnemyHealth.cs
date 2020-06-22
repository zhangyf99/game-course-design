using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    //the speed the enemy sink through the floor when dead
    public float sinkSpeed = 1.5f;
    //the amount add to the player's score when the enemy dies
    public int scoreValue = 10;
    //the sound to play when the enemy dies
    public AudioClip deathClip;
    public bool isDead;


    Animator anim;
    AudioSource enemyAudio;
    //reference to the practice system that plays when the enemy is damaged(special effect)
    ParticleSystem hitParticles;
    //reference to the capsule collider
    CapsuleCollider capsuleCollider;
    
    bool isSinking;


    void Awake ()
    {
        anim = GetComponent <Animator> ();
        //enemyAudio = GetComponent <AudioSource> ();
        hitParticles = GetComponentInChildren <ParticleSystem> ();
        capsuleCollider = GetComponent <CapsuleCollider> ();

        currentHealth = startingHealth;
    }


    void Update ()
    {
        if(isSinking)
        {
            //move the enemy down by the sinkSpeed per second
            transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }


    public void TakeDamage (int amount, Vector3 hitPoint)
    {
        if(isDead)
            return;

        //enemyAudio.Play ();

        currentHealth -= amount;
        //Debug.Log(amount);
            
        hitParticles.transform.position = hitPoint;
        hitParticles.Play();

        if(currentHealth <= 0)
        {
            Death ();
        }
    }


    void Death ()
    {
        isDead = true;

        //turn the collider into a trigger so shots can pass through it
        capsuleCollider.isTrigger = true;

        anim.SetTrigger ("Dead");

        StartSinking();

        //enemyAudio.clip = deathClip;
        //enemyAudio.Play ();
    }


    public void StartSinking ()
    {
        //find and disabled the nav mesh agent
        GetComponent <UnityEngine.AI.NavMeshAgent> ().enabled = false;
  
        //GetComponent <Rigidbody> ().isKinematic = true;
        //the state if self is sinking
        isSinking = true;
        ScoreManager.score += scoreValue;
        //sink 2s
        Destroy (gameObject, 2f);
    }
}
