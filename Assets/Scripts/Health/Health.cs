using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float staringHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;
    [SerializeField] private Behaviour[] components;
    private bool invulnerable;

    private void Awake() {
        currentHealth = staringHealth;
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(float _damage) {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, staringHealth);
        
        if(currentHealth > 0)
        {
            // player hurt
            anim.SetTrigger("hurt");
            // iframes
        }
        else
        {
            // player dead
            if(!dead)
            {
                anim.SetTrigger("die");
                GetComponent<PlayerMovement>().enabled = false;
                dead = true;
                // gameOver.gameOverScreen();
            }
        }
    }

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, staringHealth);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

    //Respawn
    public void Respawn()
    {
        AddHealth(staringHealth);
        anim.ResetTrigger("die");
        anim.Play("Idle");
        // StartCoroutine(Invunerability());
        dead = false;
    }
}
