using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;
    public AudioClip deathClip;


    Animator anim;
    AudioSource enemyAudio;
    ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;
    bool isDead;
    bool isSinking;


    void Awake ()
    {
        //mendapatkan reference koponen
        anim = GetComponent <Animator> ();
        enemyAudio = GetComponent <AudioSource> ();
        hitParticles = GetComponentInChildren <ParticleSystem> ();
        capsuleCollider = GetComponent <CapsuleCollider> ();

        //set current health
        currentHealth = startingHealth;
    }


    void Update ()
    {   
        //check jika sinking
        if (isSinking)
        {
            //memindahkan object kebawah
            transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }


    public void TakeDamage (int amount, Vector3 hitPoint)
    {   //cek jika mati
        if (isDead)
            return;

        //play audio
        enemyAudio.Play ();
        //kurangi health
        currentHealth -= amount;
        //ganti posisi particle
        hitParticles.transform.position = hitPoint;
        //play particle System
        hitParticles.Play();

        //dead jika healt <= 0
        if (currentHealth <= 0)
        {
            Death ();
        }
    }


    void Death ()
    {   
        //set isdead
        isDead = true;

        //set CapsuleCollider ke trigger
        capsuleCollider.isTrigger = true;

        //trigger play animation dead
        anim.SetTrigger ("Dead");

        //Play sound dead
        enemyAudio.clip = deathClip;
        enemyAudio.Play ();
    }


    public void StartSinking ()
    {
        //disable Navmesh Component
        GetComponent<UnityEngine.AI.NavMeshAgent> ().enabled = false;
        //set rigidbody ke kinematik
        GetComponent<Rigidbody> ().isKinematic = true;
        isSinking = true;
        ScoreManager.score += scoreValue;
        Destroy (gameObject, 2f);
    }
}
