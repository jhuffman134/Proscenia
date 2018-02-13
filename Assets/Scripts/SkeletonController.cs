using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;
using UnityEngine.UI;

public class SkeletonController : MonoBehaviour {

    public GameObject drop;
    public Image healthBar; 
    public Animator anim;
    public Rigidbody2D rb2d;
    private GameObject player;
    private PlatformerCharacter2D pc2d;
    private Collider2D[] colliders;
    public Transform AttackCheck;
    private float attackRadius = 0.15f;
    public float health;
    private float maxHealth;
    public float damageFromPlayer;
    public bool isAlive = true;
    public bool isHit = false;
    public bool isAttacking = false;
    public float damage;
    public float damagedTime;
    public float damagedTimer;
    public float recoilForce;
    public bool facingRight = true;
    public float attackDistance;
    public float speed;
    public float deathTime;
    public bool hasDied = false;
    public string name;
    
    void Start()
    {
        maxHealth = health;
        player = GameObject.Find("Player");
        pc2d = player.GetComponent<PlatformerCharacter2D>();
    }
	void Update () {
		if (isAlive)
        {
            if (Mathf.Abs(transform.position.x - GameObject.Find("Player").transform.position.x) < attackDistance)
            {
                isAttacking = true;
            }
            if (isHit)
            {
                isAttacking = false;
                if (Time.time - damagedTimer > damagedTime)
                {
                    isHit = false;
                    anim.SetBool("isHit", isHit);
                }
            } else if (isAttacking)
            {
                if (transform.position.x > player.transform.position.x)
                {
                    transform.position += speed * Time.deltaTime * Vector3.left;
                    if (facingRight && Mathf.Abs(player.transform.position.x - transform.position.x) > 0.25)
                    {
                        Flip();
                    }
                } else
                {
                    transform.position += speed * Time.deltaTime * Vector3.right;
                    if (!facingRight && Mathf.Abs(player.transform.position.x - transform.position.x) > 0.25)
                    {
                        Flip();
                    }
                }
            }
        } else {
            if (!hasDied)
            {
                hasDied = true;
                Instantiate(drop, gameObject.transform.position, gameObject.transform.rotation);
                Destroy(gameObject, deathTime);
            }
        }
        anim.SetBool("isAlive", isAlive);        
        anim.SetBool("isHit", isHit);
        anim.SetBool("isAttacking", isAttacking);
    }

    public void skeletonTakeDamage(float damage)
    {
        if (isAlive)
        {
            isHit = true;
            damagedTimer = Time.time;
            health -= damage;
            if (health / maxHealth > 0.01)
            {
                healthBar.transform.localScale = new Vector3(health / maxHealth, 1);
            } else
            {
                healthBar.gameObject.SetActive(false);
            }
            

            isAlive = (health > 0);
            if (player.transform.position.x > transform.position.x)
            {
                rb2d.AddForce(new Vector3(-1, 0.1f, 0) * recoilForce);
            } else
            {
                rb2d.AddForce(new Vector3(1, 0.1f, 0) * recoilForce);
            }
            
        }        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (isAlive)
            {
                if (rb2d.velocity.x > 0)
                {
                    rb2d.AddForce(new Vector3(-0.5f, 0.1f, 0) * recoilForce);
                }
                else
                {
                    rb2d.AddForce(new Vector3(0.5f, 0.1f, 0) * recoilForce);
                }
            }
        }
    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            pc2d = col.gameObject.GetComponent<PlatformerCharacter2D>();
            pc2d.takeDamage(pc2d.skeletonDamage);
        }
        /*
        if (col.gameObject.tag == "Player")
        {
            isHit = true;
            isAttacking = false;
            takeDamage(damageFromPlayer);
            damagedTimer = Time.time;
        }
        if (rb2d.velocity.x > 0)
        {
            rb2d.AddForce(new Vector3(-1, 0.1f, 0) * recoilForce);
        }
        else
        {
            rb2d.AddForce(new Vector3(1, 0.1f, 0) * recoilForce);
        }
        */
    }
    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void SkeletonAttack()
    {
        colliders = Physics2D.OverlapCircleAll(AttackCheck.position, attackRadius);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject.tag == "Player")
            {
                pc2d = colliders[i].gameObject.GetComponent<PlatformerCharacter2D>();
                pc2d.takeDamage(damage);
            }
        }
    }
}
