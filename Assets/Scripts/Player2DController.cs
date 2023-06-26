using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
    [RequireComponent(typeof(CharacterController))]
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
    [RequireComponent(typeof(PlayerInput))]
#endif
    public class Player2DController : MonoBehaviour
    {
        public float moveSpeed;
        public float jumpHeight;
        private float horizontalMovement;
        public float radius;

        public bool isJumping;
        public bool isGrounded;

        AudioSource audioSource;
        public AudioClip JumpingAudioClip;

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
        private PlayerInput _playerInput;
#endif

        private StarterAssetsInputs _input;
        public Rigidbody2D rb;
        private Vector3 velocity = Vector3.zero;
        public Transform groundCheck;
        public Animator animator;
        public SpriteRenderer spriteRenderer;
        public LayerMask collisionLayers;

        private void Start()
        {

            audioSource = GetComponent<AudioSource>();
            _input = GetComponent<StarterAssetsInputs>();
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
            _playerInput = GetComponent<PlayerInput>();
#else
			Debug.LogError( "Starter Assets package is missing dependencies. Please use Tools/Starter Assets/Reinstall Dependencies to fix it");
#endif
        }

        private void Update()
        {
            if (GameManager.Instance.IsPlaying)
            {
                Jump();
                Move();
                Flip(rb.velocity.x);
            }
        }

        private void FixedUpdate()
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, radius, collisionLayers);
        }

        void Move()
        {
            horizontalMovement = _input.move.x * moveSpeed * Time.fixedDeltaTime;
            Vector3 targetVelocity = new Vector2(horizontalMovement, rb.velocity.y);
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);

            float playerVelocity = Mathf.Abs(rb.velocity.x);// Renvoie la valeur absolue de la vitesse
            animator.SetFloat("Speed", playerVelocity);
        }

        void Jump()
        {
            if (_input.jump && isGrounded)
            {
                audioSource.clip = JumpingAudioClip;
                audioSource.Play();
                animator.SetTrigger("Jump");
                rb.AddForce(new Vector2(0f, jumpHeight));
                _input.jump = false;
            }
        }

        void Flip(float velocity)
        {
            if (velocity > 0.1f)
                spriteRenderer.flipX = false;

            else if (velocity < -0.1f)
                spriteRenderer.flipX = true;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, radius);
        }
    }
}
