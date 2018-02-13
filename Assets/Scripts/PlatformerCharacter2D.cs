using System;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class PlatformerCharacter2D : MonoBehaviour
    {
        [SerializeField]
        private float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
        [SerializeField]
        private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.
        //[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;  // Amount of maxSpeed applied to crouching movement. 1 = 100%
        [SerializeField]
        private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
        [SerializeField]
        private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character

        private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
        private Transform AttackCheck;
        const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
        const float attackRadius = 0.15f;
        private bool m_Grounded;            // Whether or not the player is grounded.
        const float k_CeilingRadius = 0.01f; // Radius of the overlap circle to determine if the player can stand up
        private Animator m_Anim;            // Reference to the player's animator component.
        private Rigidbody2D m_Rigidbody2D;
        private bool m_FacingRight = true;  // For determining which way the player is currently facing.
        private bool attack = false;
        Collider2D[] colliders;
        public SkeletonController sc;
        public float springJumpSpeed = 10f;
        public float jumpSpeed = 5f;

        // combat parameters
        public float damage;
        public float health;
        private float maxHealth;
        public bool isDamaged = false;
        private bool isAlive = true;
        public float skeletonDamage = 10f;
        private float isDamagedTimer;
        public float damagedTime;
        private Vector3 recoilDirection;
        public float recoilForce = 20f;        

        private void Awake()
        {
            m_GroundCheck = transform.Find("GroundCheck");
            AttackCheck = transform.Find("AttackCheck");
            m_Anim = GetComponent<Animator>();
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
            maxHealth = health;
        }
        private void Update()
        {
            m_Grounded = false;

            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            // This can be done using layers instead but Sample Assets will not overwrite your project settings.
            colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                {
                    m_Grounded = true;
                }
            }
            m_Anim.SetBool("Ground", m_Grounded);

            // Set the vertical animation
            m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);
            if (Time.time - isDamagedTimer > damagedTime)
            {
                isDamaged = false;
                m_Anim.SetBool("isDamaged", isDamaged);
            }
            m_Anim.SetBool("Attack", attack);
        }

        void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.tag == "Skeleton")
            {
                takeDamage(skeletonDamage / 2);
            }
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.tag == "Spring")
            {

                m_Rigidbody2D.velocity = new Vector2(0, springJumpSpeed);
            }
        }

        public void Move(float move, bool crouch, bool jump, bool Attack)
        {
            // If crouching, check to see if the character can stand up
            //if (!crouch && m_Anim.GetBool("Crouch"))
            //{
            //   // If the character has a ceiling preventing them from standing up, keep them crouching
            //    if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
            //    {
            //       crouch = true;
            //    }
            //}

            // Set whether or not the character is crouching in the animator
            //m_Anim.SetBool("Crouch", crouch);

            //only control the player if grounded or airControl is turned on
            if (m_Grounded || m_AirControl)
            {
                // Reduce the speed if crouching by the crouchSpeed multiplier
                //move = (crouch ? move*m_CrouchSpeed : move);

                // The Speed animator parameter is set to the absolute value of the horizontal input.
                m_Anim.SetFloat("Speed", Mathf.Abs(move));

                // Move the character
                m_Rigidbody2D.velocity = new Vector2(move * m_MaxSpeed, m_Rigidbody2D.velocity.y);

                // If the input is moving the player right and the player is facing left...
                if (move > 0 && !m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
                // Otherwise if the input is moving the player left and the player is facing right...
                else if (move < 0 && m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
            }
            // If the player should jump...
            //if (m_Grounded && jump && m_Anim.GetBool("Ground"))
            if (m_Grounded && jump)
            {
                // Add a vertical force to the player.
                m_Grounded = false;
                m_Anim.SetBool("Ground", false);
                //m_Rigidbody2D.AddForce(Vector2.up * m_JumpForce);
                m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, jumpSpeed);
            }
            attack = Attack;
        }
        private void Flip()
        {
            // Switch the way the player is labelled as facing.
            m_FacingRight = !m_FacingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }

        public float getHealth()
        {
            return health;
        }

        public float getDamage()
        {
            return damage;
        }

        public void takeDamage(float damage)
        {
            if (damage > 0)
            {
                health -= damage;
                if (health < 0)
                {
                    health = 0;
                }
                isAlive = (health > 0);
            }
            isDamaged = true;
            isDamagedTimer = Time.time;
            if (m_Rigidbody2D.velocity.x > 0)
            {
                recoilDirection = new Vector3(1, 0.1f, 0);
            }
            else
            {
                recoilDirection = new Vector3(-1, 0.1f, 0);
            }

            m_Rigidbody2D.AddForce(recoilDirection * recoilForce);

            m_Anim.SetBool("isDamaged", isDamaged);
            m_Anim.SetBool("isAlive", isAlive);
        }
        public void healDamage(float heal)
        {
            if (health > 0)
            {
                if (health + heal > maxHealth)
                {
                    health = maxHealth;
                }
                else
                {
                    health += heal;
                }
            }
        }

        public float getMaxHealth()
        {
            return maxHealth;
        }

        public void Attack()
        {
            colliders = Physics2D.OverlapCircleAll(AttackCheck.position, attackRadius);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject.tag == "Skeleton")
                {
                    sc = colliders[i].gameObject.GetComponent<SkeletonController>();
                    sc.skeletonTakeDamage(damage);
                }
            }
        }
    }
}